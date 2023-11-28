using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Instituciones.Queries;

public record BuscarInstitucion : IRequest<List<BuscarInstitucionResponse>>
{
    public string Nombre { get; set; }
    public int pagina { get; set; }
}

public class BuscarInstitucionHandler : IRequestHandler<BuscarInstitucion, List<BuscarInstitucionResponse>>
{
    private readonly ConvenioContext _context;
    
    public BuscarInstitucionHandler(ConvenioContext context)
    {
        _context = context;
    }
    
    public async Task<List<BuscarInstitucionResponse>> Handle(BuscarInstitucion request, CancellationToken cancellationToken)
    {
        var institucion = await _context.Instituciones.Where(x => x.Nombre.Contains(request.Nombre)).ToListAsync();
        
        var response = institucion
            .Skip((request.pagina - 1) * 10)
            .Take(10)
            .Select(x => new BuscarInstitucionResponse{
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

public record BuscarInstitucionResponse
{
    public int Institucion_Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Ciudad { get; set; }
    public string? Estado { get; set; }
    public string? Pais { get; set; }
    public string? Identificacion { get; set; }
    public string? Direccion { get; set; }
}