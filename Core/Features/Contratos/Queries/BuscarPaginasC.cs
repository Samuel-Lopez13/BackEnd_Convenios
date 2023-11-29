using Core.Features.Instituciones.Queries;
using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Contratos.Queries;

public record BuscarPaginasC : IRequest<BuscarPaginasCResponse>
{
    public string Nombre { get; set; }
}

public class BuscarPaginasCHandler : IRequestHandler<BuscarPaginasC, BuscarPaginasCResponse>
{
    private readonly ConvenioContext _context;
    
    public BuscarPaginasCHandler(ConvenioContext context)
    {
        _context = context;
    }
    
    public async Task<BuscarPaginasCResponse> Handle(BuscarPaginasC request, CancellationToken cancellationToken)
    {
        var contratos = await _context.Contratos.Where(x => x.Nombre.Contains(request.Nombre)).ToListAsync();
        
        var response = new BuscarPaginasCResponse{
            Paginas = (int)Math.Ceiling((double)contratos.Count / 10)
        };

        return response;
    }
}

public record BuscarPaginasCResponse
{
    public int Paginas { get; set; }
}