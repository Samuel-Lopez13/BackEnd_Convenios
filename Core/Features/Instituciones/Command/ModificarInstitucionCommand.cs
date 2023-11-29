using Core.Domain.Exceptions;
using Core.Domain.Services;
using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Instituciones.Command;

public record ModificarInstitucionCommand : IRequest
{
    public string Pais { get; set; }
    public string Estado { get; set; }
    public string Ciudad { get; set; }
    public string Identificacion { get; set; }
    public string Direccion { get; set; }
}

public class ModificarInstitucionHandler : IRequestHandler<ModificarInstitucionCommand>
{
    private readonly ConvenioContext _context;
    private readonly IAuthorization _authorization;
    
    public ModificarInstitucionHandler(ConvenioContext context, IAuthorization authorization)
    {
        _context = context;
        _authorization = authorization;
    }
    
    public async Task<Unit> Handle(ModificarInstitucionCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _context.Usuarios
            .Include(u => u.Instituciones)
            .FirstOrDefaultAsync(x => x.Usuario_Id == _authorization.UsuarioActual());
        
        var institucion = await _context.Instituciones.FirstOrDefaultAsync(x => x.Institucion_Id == usuario.Instituciones.Institucion_Id);
        
        if (institucion == null)
        {
            throw new NotFoundException("No se encontro la institucion");
        }
        
        institucion.Pais = request.Pais;
        institucion.Estado = request.Estado;
        institucion.Ciudad = request.Ciudad;
        institucion.Identificacion = request.Identificacion;
        institucion.Direccion = request.Direccion;
        
        _context.Instituciones.Update(institucion);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}