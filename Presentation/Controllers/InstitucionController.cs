using Core.Features.Instituciones.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("api/[controller]")]
public class InstitucionController : ControllerBase
{
    private readonly IMediator _mediator;

    public InstitucionController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Devuelve una lista de todos los usuarios registrados
    /// </summary>
    /// <remarks>
    /// <b>JSON:</b> todos los usuarios registrados
    /// </remarks>
    [HttpGet("ObtenerUsuarios")]
    public async Task<List<ObtenerInstitucionesResponse>> ObtenerUsuarios()
    {
        return await _mediator.Send(new ObtenerInstituciones());
    }
}