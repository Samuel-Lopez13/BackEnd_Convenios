using Core.Domain.Exceptions;
using Core.Infraestructure.Persistance;
using MediatR;

namespace Core.Features.Instituciones.Command;

public record EliminarInstitucionCommand : IRequest
{
    public int Institucion_Id { get; set; }
}

public class EliminarInstitucionHandler : IRequestHandler<EliminarInstitucionCommand>
{
    private readonly ConvenioContext _context;

    public EliminarInstitucionHandler(ConvenioContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(EliminarInstitucionCommand request, CancellationToken cancellationToken)
    {
        var institucion = await _context.Instituciones.FindAsync(request.Institucion_Id);
        
        if (institucion == null)
        {
            throw new NotFoundException("No se encontro la institucion");
        }
        
        _context.Instituciones.Remove(institucion);
        await _context.SaveChangesAsync();
        
        return Unit.Value;
    }
}