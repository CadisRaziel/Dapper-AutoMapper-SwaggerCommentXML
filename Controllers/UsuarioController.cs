using CrudDapperAndAutoMapper.DTO;
using CrudDapperAndAutoMapper.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudDapperAndAutoMapper.Controllers
{

/*
 * COMO ADICIONAR COMENTARIO NO ENDPOINT DO SWAGGER !!!

    No swagger eu adicionei comentarios na frente dos endpoint explicando para que ele serve

    para fazer isso eu precisei clicar 2x em cima do nome do projeto que aqui no caso e `CrudDapperAndAutoMapper`

    adiconei esse codigo no <PropertyGroup>
	    <GenerateDocumentationFile>true</GenerateDocumentationFile>
        <NoWarn>$(NoWarn);1591</NoWarn>

    em seguida no program.cs eu configurei o swagger
    builder.Services.AddSwaggerGen(c =>
    {
        // Defina o caminho para o arquivo XML
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
    });

     e pronto e so ir no metodo do controller e adicionar `summary`
     /// <summary>
        /// Metodo para criar usuario, Schemma "UsuarioCriarDTO"
        /// </summary>
        /// <param name="usuarioCriarDTO"></param>
        /// <returns>Ao criar usuario retorna a lista de usuarios atualizada com o novo usuario</returns>
        [HttpPost]        
        [ProducesResponseType(typeof(UsuarioCriarDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UsuarioCriarDTO), StatusCodes.Status400BadRequest)]        
        public async Task<IActionResult> CriarUsuario(UsuarioCriarDTO usuarioCriarDTO)           
        {
            var usuario = await _usuarioInterface.CriarUsuario(usuarioCriarDTO);

            if (usuario.Status == false)
            {
                return BadRequest(usuario);
            }

            return Ok(usuario);
        }
 */



    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioInterface _usuarioInterface;

        public UsuarioController(IUsuarioInterface usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }

        /// <summary>
        /// Buscar todos usuarios do sistema
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> BuscarUsuarios () {
            var usuarios = await _usuarioInterface.BuscarUsuarios();

            if(usuarios.Status == false)
            {
                return NotFound(usuarios);
            }

            return Ok(usuarios);
        } 
        /// <summary>
        /// Buscar usuario por ID
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns>Usuario especificado pelo ID</returns>
        [HttpGet("{usuarioId}")]
        public async Task<IActionResult> BuscarUsuarioPorId (int usuarioId) {

            var usuario = await _usuarioInterface.BuscarUsuarioPorId(usuarioId);

            if(usuario.Status == false)
            {
                return NotFound(usuario);
            }

            return Ok(usuario);
        }

        /// <summary>
        /// Metodo para criar usuario, Schemma "UsuarioCriarDTO"
        /// </summary>
        /// <param name="usuarioCriarDTO"></param>
        /// <returns>Ao criar usuario retorna a lista de usuarios atualizada com o novo usuario</returns>
        [HttpPost]        
        [ProducesResponseType(typeof(UsuarioCriarDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UsuarioCriarDTO), StatusCodes.Status400BadRequest)]        
        public async Task<IActionResult> CriarUsuario(UsuarioCriarDTO usuarioCriarDTO)           
        {
            var usuario = await _usuarioInterface.CriarUsuario(usuarioCriarDTO);

            if (usuario.Status == false)
            {
                return BadRequest(usuario);
            }

            return Ok(usuario);
        }

        /// <summary>
        /// Metodo para criar usuario, Schemma "UsuarioEditarDTO"
        /// </summary>
        /// <param name="usuarioEditarDTO"></param>
        /// <returns>Ao editar o usuario vai listar todos usuarios em uma lista atualizada</returns>
        [HttpPut]        
        [ProducesResponseType(typeof(UsuarioCriarDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UsuarioCriarDTO), StatusCodes.Status400BadRequest)]        
        public async Task<IActionResult> EditarUsuario(UsuarioEditarDTO usuarioEditarDTO)           
        {
            var usuario = await _usuarioInterface.EditarUsuario(usuarioEditarDTO);

            if (usuario.Status == false)
            {
                return BadRequest(usuario);
            }

            return Ok(usuario);
        }
        
        /// <summary>
        /// Metodo para remover usuario pelo ID
        /// </summary>
        /// <param name="usuarioEditarDTO"></param>
        /// <returns>Remove o usuario e retorna a lista atualizada</returns>
        [HttpDelete]        
        [ProducesResponseType(typeof(UsuarioCriarDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UsuarioCriarDTO), StatusCodes.Status400BadRequest)]        
        public async Task<IActionResult> DeletarUsuario(int usuarioId)           
        {
            var usuario = await _usuarioInterface.RemoverUsuario(usuarioId);

            if (usuario.Status == false)
            {
                return BadRequest(usuario);
            }

            return Ok(usuario);
        }

    }
}