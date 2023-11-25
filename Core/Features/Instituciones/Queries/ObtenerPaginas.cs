using Core.Domain.Services;
using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Instituciones.Queries;

public record ObtenerPaginas : IRequest<ObtenerPaginasResponse>;

public class ObtenerPaginasHandler : IRequestHandler<ObtenerPaginas, ObtenerPaginasResponse>
{
    private readonly ConvenioContext _context;
    
    public ObtenerPaginasHandler(ConvenioContext context)
    {
        _context = context;
    }
    
    public async Task<ObtenerPaginasResponse> Handle(ObtenerPaginas request, CancellationToken cancellationToken)
    {
        var instituciones = await _context.Instituciones.ToListAsync();
        
        var response = new ObtenerPaginasResponse{
            Paginas = (int)Math.Ceiling((double)instituciones.Count / 10)
        };

        return response;
    }
}

public record ObtenerPaginasResponse
{
    public int Paginas { get; set; }
}