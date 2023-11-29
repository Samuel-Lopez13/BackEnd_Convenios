using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Contratos.Queries;

public record ObtenerPaginasC : IRequest<ObtenerPaginasCResponse>;

public class ObtenerPaginasCHandler : IRequestHandler<ObtenerPaginasC, ObtenerPaginasCResponse>
{
    private readonly ConvenioContext _context;
    
    public ObtenerPaginasCHandler(ConvenioContext context)
    {
        _context = context;
    }
    
    public async Task<ObtenerPaginasCResponse> Handle(ObtenerPaginasC request, CancellationToken cancellationToken)
    {
        var contratos = await _context.Contratos.ToListAsync();
        
        var response = new ObtenerPaginasCResponse{
            Paginas = (int)Math.Ceiling((double)contratos.Count / 10)
        };

        return response;
    }
}

public record ObtenerPaginasCResponse
{
    public int Paginas { get; set; }
}