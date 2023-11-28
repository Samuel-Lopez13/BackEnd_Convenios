using Core.Domain.Exceptions;
using Core.Domain.Services;
using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Usuarios.Queries;

public record ObtenerUsuarios : IRequest<List<ObtenerUsuariosResponse>>
{
    public int pagina { get; set; }
}

public class ObtenerUsuariosHandler : IRequestHandler<ObtenerUsuarios, List<ObtenerUsuariosResponse>>
{
    private readonly ConvenioContext _context;
    private readonly IAuthorization _authorization;
    
    public ObtenerUsuariosHandler(ConvenioContext context, IAuthorization authorization)
    {
        _context = context;
        _authorization = authorization;
    }
    
    public async Task<List<ObtenerUsuariosResponse>> Handle(ObtenerUsuarios request, CancellationToken cancellationToken)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Usuario_Id == _authorization.UsuarioActual());
        
        //Verifica que el usario sea administrador
        if(usuario.Rol_Id != 1)
            throw new ForbiddenAccessException();
        
        var response = _context.Usuarios
            .Skip((request.pagina - 1) * 10)
            .Take(10)
            .Select(x => new ObtenerUsuariosResponse{
                Usuario_Id = x.Usuario_Id,
                Email = x.Email,
                Nombre = x.Nombre,
                Institucion = x.Instituciones.Nombre
            }).ToList();

        return response;
    }
}

public record ObtenerUsuariosResponse
{
    public int Usuario_Id { get; set; }
    public string Email { get; set; } = null!;
    public string Nombre { get; set; } = null!;
    public string Institucion { get; set; }
}