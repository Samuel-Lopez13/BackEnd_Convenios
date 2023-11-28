using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Usuarios.Queries;

public record ObtenerPaginasU : IRequest<ObtenerPaginasUResponse>;

public class ObtenerPaginasUHandler : IRequestHandler<ObtenerPaginasU, ObtenerPaginasUResponse>
{
    private readonly ConvenioContext _context;
    
    public ObtenerPaginasUHandler(ConvenioContext context)
    {
        _context = context;
    }
    
    public async Task<ObtenerPaginasUResponse> Handle(ObtenerPaginasU request, CancellationToken cancellationToken)
    {
        var usuarios = await _context.Usuarios.ToListAsync();
        
        var response = new ObtenerPaginasUResponse{
            Paginas = (int)Math.Ceiling((double)usuarios.Count / 10)
        };

        return response;
    }
}

public record ObtenerPaginasUResponse
{
    public int Paginas { get; set; }
}