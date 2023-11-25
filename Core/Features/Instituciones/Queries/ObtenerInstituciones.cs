using Core.Domain.Exceptions;
using Core.Domain.Services;
using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Instituciones.Queries;

public record ObtenerInstituciones : IRequest<List<ObtenerInstitucionesResponse>>
{
    public int pagina { get; set; }
}

public class ObtenerInstitucionesHandler : IRequestHandler<ObtenerInstituciones, List<ObtenerInstitucionesResponse>>
{
    private readonly ConvenioContext _context;
    private readonly IAuthorization _authorization;
    
    public ObtenerInstitucionesHandler(ConvenioContext context, IAuthorization authorization)
    {
        _context = context;
        _authorization = authorization;
    }
    
    public async Task<List<ObtenerInstitucionesResponse>> Handle(ObtenerInstituciones request, CancellationToken cancellationToken)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Usuario_Id == _authorization.UsuarioActual());
        
        //Verifica que el usario sea administrador
        if(usuario.Rol_Id != 1)
            throw new ForbiddenAccessException();
        
        //Devuelve una lista de usuarios
        var instituciones = await _context.Instituciones.ToListAsync();

        var response = instituciones
        .Skip((request.pagina - 1) * 10)
        .Take(10)
        .Select(x => new ObtenerInstitucionesResponse{
            Institucion_Id = x.Institucion_Id,
            Nombre = x.Nombre,
            Ciudad = x.Ciudad,
            Estado = x.Estado,
            Pais = x.Pais,
            Identificacion = x.Identificacion,
            Direccion = x.Direccion
        }).ToList();

        return response;
    }
}

public record ObtenerInstitucionesResponse
{
    public int Institucion_Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Ciudad { get; set; }
    public string? Estado { get; set; }
    public string? Pais { get; set; }
    public string? Identificacion { get; set; }
    public string? Direccion { get; set; }
}