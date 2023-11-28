using Core.Domain.Exceptions;
using Core.Features.Instituciones.Command;
using Core.Infraestructure.Persistance;
using MediatR;

namespace Core.Features.Usuarios.Command;

public record EliminarUsuarioCommand : IRequest
{
    public int Usuario_Id { get; set; }
}

public class EliminarUsuarioHandler : IRequestHandler<EliminarUsuarioCommand>
{
    private readonly ConvenioContext _context;

    public EliminarUsuarioHandler(ConvenioContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(EliminarUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _context.Usuarios.FindAsync(request.Usuario_Id);
        
        if (usuario == null)
        {
            throw new NotFoundException("No se encontro la institucion");
        }
        
        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
        
        return Unit.Value;
    }
}