using Core.Features.Instituciones.Queries;
using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Usuarios.Queries;

public record BuscarUsuarios : IRequest<List<BuscarUsuariosResponse>>
{
    public string Nombre { get; set; }
    public int Pagina { get; set; }
}

public class BuscarUsuariosHandler : IRequestHandler<BuscarUsuarios, List<BuscarUsuariosResponse>>
{
    private readonly ConvenioContext _context;

    public BuscarUsuariosHandler(ConvenioContext context)
    {
        _context = context;
    }
    
    public async Task<List<BuscarUsuariosResponse>> Handle(BuscarUsuarios request, CancellationToken cancellationToken)
    {
        var usuario = await _context.Usuarios
            .Include(x => x.Instituciones)
            .Where(x => x.Nombre.Contains(request.Nombre)).ToListAsync();
        
        var response = usuario
            .Skip((request.Pagina - 1) * 10)
            .Take(10)
            .Select(x => new BuscarUsuariosResponse{
                Usuario_Id = x.Usuario_Id,
                Nombre = x.Nombre,
                Email = x.Email,
                Institucion = x.Instituciones.Nombre
            }).ToList();

        return response;
    }
}

public record BuscarUsuariosResponse
{
    public int Usuario_Id { get; set; }
    public string Email { get; set; } = null!;
    public string Nombre { get; set; } = null!;
    public string Institucion { get; set; }
}