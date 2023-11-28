using Core.Features.Instituciones.Command;
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
    /// Devuelve una lista de todos las instituciones buscadas por nombre
    /// </summary>
    /// <remarks>
    /// <b>JSON:</b> todos las instituciones que coincidan con el nombre
    /// </remarks>
    [HttpGet("Buscar")]
    public async Task<List<BuscarInstitucionResponse>> GetBuscarInstituciones(int pagina, string nombre)
    {
        return await _mediator.Send(new BuscarInstitucion(){ pagina = pagina, Nombre = nombre });
    }
    
    /// <summary>
    /// Devuelve el total de paginas de las instituciones buscadas por nombre
    /// </summary>
    /// <remarks>
    /// <b>JSON:</b> Numero de paginas
    /// </remarks>
    [HttpGet("Paginas/{nombre}")]
    public async Task<BuscarPaginasIResponse> GetBuscarPaginas(string nombre)
    {
        return await _mediator.Send(new BuscarPaginasI(){Nombre = nombre});
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
    public async Task<ObtenerPaginasIResponse> GetPaginas()
    {
        return await _mediator.Send(new ObtenerPaginasI());
    }
    
    /// <summary>
    /// Crea una institucion
    /// </summary>
    /// <remarks>
    /// <b>JSON:</b> Institucion creada
    /// </remarks>
    [HttpPost("Institucion")]
    public async Task<IActionResult> PostInstitucion([FromBody] CrearInstitucionCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
    
    /// <summary>
    /// Elimina una institucion
    /// </summary>
    /// <remarks>
    /// <b>JSON:</b> Institucion eliminada
    /// <br/><br/>
    /// <b>404:</b> Not Found: No se encontro la institucion
    /// </remarks>
    [HttpDelete("Institucion/{Institucion_Id}")]
    public async Task<IActionResult> DeleteInstitucion(int Institucion_Id)
    {
        return Ok(await _mediator.Send(new EliminarInstitucionCommand(){ Institucion_Id = Institucion_Id }));
    }
    
}