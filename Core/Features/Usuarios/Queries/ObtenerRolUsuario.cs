using Core.Domain.Services;
using Core.Features.Instituciones.Queries;
using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Usuarios.Queries;

public record ObtenerRolUsuario : IRequest<ObtenerRolUsuarioResponse>;

public class ObtenerRolUsuarioHandler : IRequestHandler<ObtenerRolUsuario, ObtenerRolUsuarioResponse>
{
    private readonly ConvenioContext _context;
    private readonly IAuthorization _authorization;
    
    public ObtenerRolUsuarioHandler(ConvenioContext context, IAuthorization authorization)
    {
        _context = context;
        _authorization = authorization;
    }
    
    public async Task<ObtenerRolUsuarioResponse> Handle(ObtenerRolUsuario request, CancellationToken cancellationToken)
    {
        var usuario = await _context.Usuarios
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(x => x.Usuario_Id == _authorization.UsuarioActual());
        
        var response = new ObtenerRolUsuarioResponse{
            Rol = usuario?.Roles?.Nombre ?? "Sin Rol"
        };

        return response;
    }
}

public record ObtenerRolUsuarioResponse
{
    public string Rol { get; set; }
}