using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Contratos.Command;

public record RevisionCommand : IRequest
{
    public int Id_Contrato { get; set; }
}

public class RevisionCommandHandler : IRequestHandler<RevisionCommand>
{
    private readonly ConvenioContext _context;
    
    public RevisionCommandHandler(ConvenioContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(RevisionCommand request, CancellationToken cancellationToken)
    {
        var fase = await _context.Intercambios.FirstOrDefaultAsync(x => x.Contrato_Id == request.Id_Contrato);

        fase.Revision = !fase.Revision;

        _context.Intercambios.Update(fase);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}