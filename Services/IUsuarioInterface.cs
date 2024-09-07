using CrudDapperAndAutoMapper.DTO;
using CrudDapperAndAutoMapper.Models;

namespace CrudDapperAndAutoMapper.Services
{
    public interface IUsuarioInterface
     {

        //Task -> metodo asyncrono
        //Aqui as vezes nao queremos passar o Model pois tera informacoes sigilosas como senha ou outra coisa que eu nao quero que quem consumir a api veja
        //E aqui entao que entra em cena os DTOS
        //UsuarioListarDTO tem quase as mesmas coisa que o Model Usuario porem eu removi alguns parametros que eu nao quero que quem consuma a api veja
        Task<ResponseModel<List<UsuarioListarDTO>>> BuscarUsuarios();
        Task<ResponseModel<UsuarioListarDTO>> BuscarUsuarioPorId(int usuarioId);
        Task<ResponseModel<List<UsuarioListarDTO>>> CriarUsuario(UsuarioCriarDTO usuarioCriarDTO);
        Task<ResponseModel<List<UsuarioListarDTO>>> EditarUsuario(UsuarioEditarDTO usuarioEditarDTO);
        Task<ResponseModel<List<UsuarioListarDTO>>> RemoverUsuario (int usuarioId);
    }
}


//O `Controller` pede para a `Interface` um metodo a `Interface` pede para o servico a implementacao desse metodo e o servico retorna o valor de toda implementacao 