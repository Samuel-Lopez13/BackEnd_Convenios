using Core.Features.Contratos.Command;
using Core.Features.Contratos.Queries;
using Core.Features.Instituciones.Command;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("[controller]")]
public class ContratosController : ControllerBase
{
    private readonly IMediator _mediator;

    public ContratosController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("ContratosFile/{Contratos_Id}")]
    public async Task<ContratoIdResponse> GetContratosId(int Contratos_Id)
    {
        return await _mediator.Send(new ContratosId(){ Contrato_Id = Contratos_Id });
    }
    
    /// <summary>
    /// Devuelve una lista de todos los contratos buscados por nombre
    /// </summary>
    /// <remarks>
    /// <b>JSON:</b> todos los contratos que coincidan con el nombre
    /// </remarks>
    [HttpGet("Buscar")]
    public async Task<List<BuscarContratosResponse>> GetBuscarInstituciones(int pagina, string nombre)
    {
        return await _mediator.Send(new BuscarContratos(){ pagina = pagina, Nombre = nombre });
    }
    
    /// <summary>
    /// Devuelve el total de paginas de los contratos buscados por nombre
    /// </summary>
    /// <remarks>
    /// <b>JSON:</b> Numero de paginas
    /// </remarks>
    [HttpGet("Paginas/{nombre}")]
    public async Task<BuscarPaginasCResponse> GetBuscarPaginas(string nombre)
    {
        return await _mediator.Send(new BuscarPaginasC(){Nombre = nombre});
    }
    
    /// <summary>
    /// Devuelve una lista de todos los contratos registradas
    /// </summary>
    /// <remarks>
    /// <b>JSON:</b> todos los contratos registradas
    /// </remarks>
    [HttpGet("Contratos/{pagina}")]
    public async Task<List<ObtenerContratosResponse>> GetInstituciones(int pagina)
    {
        return await _mediator.Send(new ObtenerContratos(){ pagina = pagina });
    }
    
    /// <summary>
    /// Devuelve el total de paginas
    /// </summary>
    /// <remarks>
    /// <b>JSON:</b> Numero de paginas
    /// </remarks>
    [HttpGet("Paginas")]
    public async Task<ObtenerPaginasCResponse> GetPaginas()
    {
        return await _mediator.Send(new ObtenerPaginasC());
    }
    
    [HttpGet("ContratosUsuario")]
    public async Task<List<ContratosUsuariosResponse>> GetContratosUsuario()
    {
        return await _mediator.Send(new ContratosUsuario());
    }
    
    [HttpGet("Revision/{Contrato_Id}")]
    public async Task<RevisionResponse> GetRevision(int Contrato_Id)
    {
        return await _mediator.Send(new Revision(){ Contrato_Id = Contrato_Id });
    }
    
    [HttpGet("Firma/{Contrato_Id}")]
    public async Task<FirmaResponse> GetFirma(int Contrato_Id)
    {
        return await _mediator.Send(new Firma(){ Contrato_Id = Contrato_Id });
    }
    
    /// <summary>
    /// Crea un contrato
    /// </summary>
    /// <remarks>
    /// <b>200:</b> Ok institucion creada con exito
    /// </remarks>
    [HttpPost("Contrato")]
    public async Task<IActionResult> PostContrato([FromForm] CrearContratoCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
    
    /*[HttpPut("Revision")]
    public async Task<IActionResult> PutRevision([FromBody] RevisionCommand command)
    {
        return Ok(await _mediator.Send(command));
    }*/
    
    [HttpPut("Firma")]
    public async Task<IActionResult> PutFirma([FromBody] FirmaCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
    
    [HttpPut("Contratos")]
    public async Task<IActionResult> PutContratos([FromForm] ModifcarContratosId command)
    {
        return Ok(await _mediator.Send(command));
    }
    
    /// <summary>
    /// Elimina un contrato
    /// </summary>
    /// <remarks>
    /// <b>200:</b> contrato eliminado
    /// <br/><br/>
    /// <b>404:</b> Not Found: No se encontro el contrato
    /// </remarks>
    [HttpDelete("Contrato/{Contrato_Id}")]
    public async Task<IActionResult> DeleteContrato(int Contrato_Id)
    {
        return Ok(await _mediator.Send(new EliminarContratoCommand(){ Contrato_Id = Contrato_Id }));
    }
}