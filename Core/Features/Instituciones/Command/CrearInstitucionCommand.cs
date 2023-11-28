using Core.Infraestructure.Persistance;
using MediatR;
using Presentation;

namespace Core.Features.Instituciones.Command;

public record CrearInstitucionCommand : IRequest
{
    public string Nombre { get; set; }
}

public class CrearInstitucionCommandHandler : IRequestHandler<CrearInstitucionCommand>
{
    private readonly ConvenioContext _context;
    
    public CrearInstitucionCommandHandler(ConvenioContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(CrearInstitucionCommand request, CancellationToken cancellationToken)
    {
        var institucion = new Institucion{
            Nombre = request.Nombre
        };
        
        await _context.Instituciones.AddAsync(institucion);
        await _context.SaveChangesAsync();
        
        return Unit.Value;
    }
}