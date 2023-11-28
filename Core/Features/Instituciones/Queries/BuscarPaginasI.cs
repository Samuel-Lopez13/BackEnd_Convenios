using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Instituciones.Queries;

public record BuscarPaginasI : IRequest<BuscarPaginasIResponse>
{
    public string Nombre { get; set; }
}

public class BuscarPaginasIHandler : IRequestHandler<BuscarPaginasI, BuscarPaginasIResponse>
{
    private readonly ConvenioContext _context;
    
    public BuscarPaginasIHandler(ConvenioContext context)
    {
        _context = context;
    }
    
    public async Task<BuscarPaginasIResponse> Handle(BuscarPaginasI request, CancellationToken cancellationToken)
    {
        var instituciones = await _context.Instituciones.Where(x => x.Nombre.Contains(request.Nombre)).ToListAsync();
        
        var response = new BuscarPaginasIResponse{
            Paginas = (int)Math.Ceiling((double)instituciones.Count / 10)
        };

        return response;
    }
}
public record BuscarPaginasIResponse
{
    public int Paginas { get; set; }
}