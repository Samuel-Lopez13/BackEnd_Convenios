using Core.Features.Usuarios.Command;
using Core.Features.Usuarios.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsuarioController(IMediator mediator)
    {
        _mediator = mediator;
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
}