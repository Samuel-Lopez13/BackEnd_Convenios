using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Contratos.Queries;

public record BuscarContratos : IRequest<List<BuscarContratosResponse>>
{
    public string Nombre { get; set; }
    public int pagina { get; set; }
}

public class BuscarContratosHandler : IRequestHandler<BuscarContratos, List<BuscarContratosResponse>>
{
    private readonly ConvenioContext _context;
    
    public BuscarContratosHandler(ConvenioContext context)
    {
        _context = context;
    }
    
    public async Task<List<BuscarContratosResponse>> Handle(BuscarContratos request, CancellationToken cancellationToken)
    {
        var contratos = await _context.Contratos
            .Include(u => u.Instituciones)
            .Where(x => x.Nombre.Contains(request.Nombre)).ToListAsync();
        
        var response = contratos
            .Skip((request.pagina - 1) * 10)
            .Take(10)
            .Select(x => new BuscarContratosResponse{
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

public record BuscarContratosResponse
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