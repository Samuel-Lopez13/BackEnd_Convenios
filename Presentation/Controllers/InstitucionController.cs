using Core.Features.Instituciones.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("[controller]")]
public class InstitucionController : ControllerBase
{
    private readonly IMediator _mediator;

    public InstitucionController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Devuelve una lista de todos las instituciones registradas
    /// </summary>
    /// <remarks>
    /// <b>JSON:</b> todos las instituciones registradas
    /// </remarks>
    [HttpGet("Instituciones/{pagina}")]
    public async Task<List<ObtenerInstitucionesResponse>> GetInstituciones(int pagina)
    {
        return await _mediator.Send(new ObtenerInstituciones(){ pagina = pagina });
    }
    
    /// <summary>
    /// Devuelve el total de paginas
    /// </summary>
    /// <remarks>
    /// <b>JSON:</b> Numero de paginas
    /// </remarks>
    [HttpGet("Paginas")]
    public async Task<ObtenerPaginasResponse> GetPaginas()
    {
        return await _mediator.Send(new ObtenerPaginas());
    }
}