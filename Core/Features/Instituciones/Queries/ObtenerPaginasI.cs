using Core.Domain.Services;
using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Instituciones.Queries;

public record ObtenerPaginasI : IRequest<ObtenerPaginasIResponse>;

public class ObtenerPaginasIHandler : IRequestHandler<ObtenerPaginasI, ObtenerPaginasIResponse>
{
    private readonly ConvenioContext _context;
    
    public ObtenerPaginasIHandler(ConvenioContext context)
    {
        _context = context;
    }
    
    public async Task<ObtenerPaginasIResponse> Handle(ObtenerPaginasI request, CancellationToken cancellationToken)
    {
        var instituciones = await _context.Instituciones.ToListAsync();
        
        var response = new ObtenerPaginasIResponse{
            Paginas = (int)Math.Ceiling((double)instituciones.Count / 10)
        };

        return response;
    }
}

public record ObtenerPaginasIResponse
{
    public int Paginas { get; set; }
}