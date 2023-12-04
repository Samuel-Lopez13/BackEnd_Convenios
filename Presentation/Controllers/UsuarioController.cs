using Core.Features.Chat.Command;
using Core.Features.Usuarios.Command;
using Core.Features.Usuarios.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsuarioController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [AllowAnonymous]
    [HttpGet("Correo")]
    public async Task<IActionResult> GetUsuario(string Correo)
    {
        return Ok(await _mediator.Send(new CorreoCommand() { Correo = Correo }));
    }
    
    /// <summary>
    /// Devuelve el rol del usuario actual
    /// </summary>
    /// <remarks>
    /// <b>JSON:</b> El rol del usuario
    /// </remarks>
    [HttpGet("Rol")]
    public async Task<ObtenerRolUsuarioResponse> GetRol()
    {
        return await _mediator.Send(new ObtenerRolUsuario());
    }
    
    /// <summary>
    /// Devuelve una lista de todos los usuarios registradas
    /// </summary>
    /// <remarks>
    /// <b>JSON:</b> todos los usuarios registradas
    /// </remarks>
    [HttpGet("Usuarios/{pagina}")]
    public async Task<List<ObtenerUsuariosResponse>> GetUsuarios(int pagina)
    {
        return await _mediator.Send(new ObtenerUsuarios(){ pagina = pagina });
    }
    
    /// <summary>
    /// Devuelve el total de paginas
    /// </summary>
    /// <remarks>
    /// <b>JSON:</b> Numero de paginas
    /// </remarks>
    [HttpGet("Paginas")]
    public async Task<ObtenerPaginasUResponse> GetPaginas()
    {
        return await _mediator.Send(new ObtenerPaginasU());
    }
    
    /// <summary>
    /// Devuelve una lista de todos los usuarios buscadas por nombre
    /// </summary>
    /// <remarks>
    /// <b>JSON:</b> todos los usuarios que coincidan con el nombre
    /// </remarks>
    [HttpGet("Buscar")]
    public async Task<List<BuscarUsuariosResponse>> GetBuscarInstituciones(int pagina, string nombre)
    {
        return await _mediator.Send(new BuscarUsuarios(){ Pagina = pagina, Nombre = nombre });
    }
    
    /// <summary>
    /// Devuelve el total de paginas de los usuarios buscados por nombre
    /// </summary>
    /// <remarks>
    /// <b>JSON:</b> Numero de paginas
    /// </remarks>
    [HttpGet("Paginas/{nombre}")]
    public async Task<BuscarPaginasUResponse> GetBuscarPaginas(string nombre)
    {
        return await _mediator.Send(new BuscarPaginasU(){Nombre = nombre});
    }
    
    /// <summary>
    /// Inicio de sesion
    /// </summary>
    /// <remarks>
    /// <b>JSON:</b> Devuelve el token de acceso
    /// <br/><br/>
    /// <b>400:</b> Si el usuario no se encuentra registrado
    /// </remarks>
    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<LoginCommandResponse> Login([FromBody] LoginCommand command)
    {
        return await _mediator.Send(command);
    }

    /// <summary>
    /// Crea un nuevo usuario
    /// </summary>
    /// <remarks>
    /// <b>JSON:</b> Correo y Contraseña del usuario creado
    /// </remarks>
    [HttpPost("Usuarios")]
    public async Task<CrearUsuarioResponse> PostUsuario([FromBody] CrearUsuarioCommand command)
    {
        return await _mediator.Send(command);
    }
    
    /// <summary>
    /// Elimina un usuario
    /// </summary>
    /// <remarks>
    /// <b>JSON:</b> Usuario eliminada
    /// <br/><br/>
    /// <b>404:</b> Not Found: No se encontro el usuario
    /// </remarks>
    [HttpDelete("Usuario/{Usuario_Id}")]
    public async Task<IActionResult> DeleteUsuario(int Usuario_Id)
    {
        return Ok(await _mediator.Send(new EliminarUsuarioCommand(){ Usuario_Id = Usuario_Id }));
    }
}