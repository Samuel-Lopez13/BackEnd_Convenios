using Core.Features.Instituciones.Queries;
using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Usuarios.Queries;

public record BuscarPaginasU : IRequest<BuscarPaginasUResponse>
{
    public string Nombre { get; set; }
}

public class BuscarPaginasUHandler : IRequestHandler<BuscarPaginasU, BuscarPaginasUResponse>
{
    private readonly ConvenioContext _context;
    
    public BuscarPaginasUHandler(ConvenioContext context)
    {
        _context = context;
    }
    
    public async Task<BuscarPaginasUResponse> Handle(BuscarPaginasU request, CancellationToken cancellationToken)
    {
        var uusuarios = await _context.Usuarios.Where(x => x.Nombre.Contains(request.Nombre)).ToListAsync();
        
        var response = new BuscarPaginasUResponse{
            Paginas = (int)Math.Ceiling((double)uusuarios.Count / 10)
        };

        return response;
    }
}

public record BuscarPaginasUResponse
{
    public int Paginas { get; set; }
}