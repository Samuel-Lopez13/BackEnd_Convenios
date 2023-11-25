using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Instituciones.Queries;

public record ObtenerInstituciones : IRequest<List<ObtenerInstitucionesResponse>>
{
}

public class ObtenerInstitucionesHandler : IRequestHandler<ObtenerInstituciones, List<ObtenerInstitucionesResponse>>
{
    private readonly ConvenioContext _context;
    
    public ObtenerInstitucionesHandler(ConvenioContext context)
    {
        _context = context;
    }
    
    public async Task<List<ObtenerInstitucionesResponse>> Handle(ObtenerInstituciones request, CancellationToken cancellationToken)
    {
        var institucions = await _context.Instituciones.ToListAsync();

        var response = institucions.Select(x => new ObtenerInstitucionesResponse{
            Institucion_Id = x.Institucion_Id,
            Nombre = x.Nombre,
            Ciudad = x.Ciudad,
            Estado = x.Estado,
            Pais = x.Pais,
            Identificacion = x.Identificacion,
            Direccion = x.Direccion
        }).ToList();

        return response;
    }
}

public record ObtenerInstitucionesResponse
{
    public int Institucion_Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Ciudad { get; set; }
    public string? Estado { get; set; }
    public string? Pais { get; set; }
    public string? Identificacion { get; set; }
    public string? Direccion { get; set; }
}