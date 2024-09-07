using AutoMapper;
using CrudDapperAndAutoMapper.DTO;
using CrudDapperAndAutoMapper.Models;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CrudDapperAndAutoMapper.Services
{
    //Packages instalador
    //Dapper (ORM que sera usado ao inves do EF)
    //System.Data.SqlClient (Da suporte ao sql)
    //AutoMapper (Para transormar objetos), porem lembre-se ele rouba um pouco da performance, OBS: tem que configura no Program.cs
    public class UsuarioService : IUsuarioInterface
    {
        private readonly IConfiguration _configuration;       
        private readonly IMapper _imapper;       

        public UsuarioService(IConfiguration configuration, IMapper imapper)
        {
            _configuration = configuration;
            _imapper = imapper;
        }        

        public async Task<ResponseModel<List<UsuarioListarDTO>>> BuscarUsuarios()
        {

            ResponseModel<List<UsuarioListarDTO>> response = new ResponseModel<List<UsuarioListarDTO>>(); //Criei uma variavel response de tipagem ResponseModel<List<UsuarioListarDTO>> VAZIA, pois ela sera preenchida nos metodos abaixo

            //Criando conexao com o banco
            //Using -> Vai abrir fazer o que tem que ser feito e fechar ao terminar o banco (automaticamente) e tambem precisamos passar a conexao pra ele
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                IEnumerable<Usuario> usuariosBanco = await connection.QueryAsync<Usuario>("SELECT * FROM Usuarios");

                if (usuariosBanco.Count() < 0) {
                    response.Mensagem = "Nenhum usuario encontrado";
                    response.Status = false;
                    return response;
                }

                //Como eu preciso de uma <List<UsuarioListarDTO>> porem meu metodo retorna um Usuario(esta ali na query)
                //eu preciso utilizar um mapper ou algo que transforme o Usuario em <List<UsuarioListarDTO>>
                var usuarioMapeado = _imapper.Map<List<UsuarioListarDTO>>(usuariosBanco); //-> Completando a transformacao do Usuario em um List<UsuarioDTO>

                response.Dados = usuarioMapeado; //Lembra que o dados e um tipo generico, ou seja eu posso atribuir qualquer classe a ele.
                response.Mensagem = "Usuarios encontrados com sucesso";

            }
            return response;
        }


        public async Task<ResponseModel<UsuarioListarDTO>> BuscarUsuarioPorId(int usuarioId)
        {
            ResponseModel<UsuarioListarDTO> response = new ResponseModel<UsuarioListarDTO>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                //WHERE Id = @Id", new {Id = usuarioId }
                //Id = tabela no banco
                //@Id o Id que iremos passar para a query, porem o nosso Id se chama usuarioID
                //como nossa variavel Id se chama usuarioId, dizemos a ele para ao ver o @Id pegar de usuarioId `new {Id = usuarioId }`
                Usuario usuariosBanco = await connection.QueryFirstOrDefaultAsync<Usuario>("SELECT * FROM Usuarios WHERE Id = @Id", new {Id = usuarioId });

                if(usuariosBanco == null)
                {
                    response.Mensagem = "Nenhum usuario encontrado";
                    response.Status = false;
                    return response;
                }

                var usuarioMapeado = _imapper.Map<UsuarioListarDTO>(usuariosBanco);

                response.Dados = usuarioMapeado; 
                response.Mensagem = "Usuario encontrado com sucesso";
            }
            return response;
        }

        public async Task<ResponseModel<List<UsuarioListarDTO>>> CriarUsuario(UsuarioCriarDTO usuarioCriarDTO)
        {
            ResponseModel<List<UsuarioListarDTO>> response = new ResponseModel<List<UsuarioListarDTO>>();
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                //ExecuteAsync apenas vai realizar uma operacao, nao vai retornar dados
                //Aqui vai criar o usuario !!
                var criarUsuario = await connection.ExecuteAsync("INSERT INTO Usuarios (NomeCompleto, Email, Cargo, Salario, CPF, Situacao, Senha) VALUES (@NomeCompleto, @Email, @Cargo, @Salario, @CPF, @Situacao, @Senha);", usuarioCriarDTO);
                //usuarioCriarDTO -> Diferente do modo que fiz acima, como eu tenho TODAS essas propriedades no meu objeto `usuarioCriarDTO` nao tem necessidade de eu realizar new {NomeCompleto = NomeCompleto...} eu passo o objeto todo que funcionara
            
                //Se teve uma row affects e que deu certo, por isso o == 0, vou validar == 0 nao funcionou e == 1 funcionou
                if(criarUsuario == 0)
                {
                    response.Mensagem = "Ocorreu um erro ao realizar um registro";
                    response.Status = false;
                    return response;
                }

                //Metodo para retornar os usuarios em uma lista atualizada com o novo usuario criado 
                var usuarios = await ListarUsuarios(connection);

                //transformar
                var usariosMapeados = _imapper.Map<List<UsuarioListarDTO>>(usuarios);

                response.Dados = usariosMapeados;
                response.Mensagem = "Usuario cadastrado com sucesso";
            }
            return response;
        }

        public async Task<ResponseModel<List<UsuarioListarDTO>>> EditarUsuario(UsuarioEditarDTO usuarioEditarDTO)
        {
            ResponseModel<List<UsuarioListarDTO>> response = new ResponseModel<List<UsuarioListarDTO>>();
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var editarUsuario = await connection.ExecuteAsync("UPDATE Usuarios SET NomeCompleto = @NomeCompleto, Email = @Email, Cargo = @Cargo, Salario = @Salario, CPF = @CPF, Situacao = @Situacao WHERE Id = @Id", usuarioEditarDTO);

                //Se teve uma row affects e que deu certo, por isso o == 0, vou validar == 0 nao funcionou e == 1 funcionou
                if (editarUsuario == 0)
                {
                    response.Mensagem = "Ocorreu um erro ao realizar a edicao do registro";
                    response.Status = false;
                    return response;
                }

                var usuarios = await ListarUsuarios(connection);

                var usariosMapeados = _imapper.Map<List<UsuarioListarDTO>>(usuarios);

                response.Dados = usariosMapeados;
                response.Mensagem = "Usuario editado com sucesso";

            }
            return response;
        }

        public async Task<ResponseModel<List<UsuarioListarDTO>>> RemoverUsuario(int usuarioId)
        {
            ResponseModel<List<UsuarioListarDTO>> response = new ResponseModel<List<UsuarioListarDTO>>();
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var removerUsuario = await connection.ExecuteAsync("DELETE FROM Usuarios WHERE Id = @Id", new { Id = usuarioId});

                if (removerUsuario == 0)
                {
                    response.Mensagem = "Ocorreu um erro ao realizar a edicao do registro";
                    response.Status = false;
                    return response;
                }

                var usuarios = await ListarUsuarios(connection);

                var usariosMapeados = _imapper.Map<List<UsuarioListarDTO>>(usuarios);

                response.Dados = usariosMapeados;
                response.Mensagem = "Usuario deletado com sucesso";

            }
            return response;
        }


        private static async Task<IEnumerable<Usuario>> ListarUsuarios(SqlConnection connection)
        {
            //Metodo para eu listar os usuarios sem precisar chamar a api criada acima
            return await connection.QueryAsync<Usuario>("SELECT * FROM Usuarios");
        }        
    }
}
