using Core.Domain.Services;
using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Contratos.Queries;

public record ObtenerContratos : IRequest<List<ObtenerContratosResponse>>
{
    public int pagina { get; set; }
}

public class ObtenerContratosHandler : IRequestHandler<ObtenerContratos, List<ObtenerContratosResponse>>
{
    private readonly ConvenioContext _context;
    
    public ObtenerContratosHandler(ConvenioContext context)
    {
        _context = context;
    }
    
    public async Task<List<ObtenerContratosResponse>> Handle(ObtenerContratos request, CancellationToken cancellationToken)
    {
        //Devuelve una lista de usuarios
        var contratos = await _context.Contratos
            .Include(u => u.Instituciones)
            .ToListAsync();
        
        var response = contratos
            .Skip((request.pagina - 1) * 10)
            .Take(10)
            .Select(x => new ObtenerContratosResponse{
                Contrato_Id = x.Contrato_Id,
                Nombre = x.Nombre,
                Descripcion = x.Descripcion,
                FechaCreacion = x.FechaCreacion,
                FechaFinalizacion = x.FechaFinalizacion,
                NombreFile = x.Nombre.Trim().ToLower() + ".docx",
                File = x.File,
                Institucion = x.Instituciones?.Nombre ?? "Sin institucion"
            }).ToList();

        return response;
    }
}

public record ObtenerContratosResponse
{
    public int Contrato_Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaFinalizacion { get; set; }
    public string? File { get; set; }
    public string NombreFile { get; set; }
    public string Institucion { get; set; } = null!;
}