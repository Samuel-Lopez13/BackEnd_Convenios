using Core.Domain.Exceptions;
using Core.Domain.Services;
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
    private readonly IEmail _email;
    
    public RevisionCommandHandler(ConvenioContext context, IEmail email)
    {
        _context = context;
        _email = email;
    }

    public async Task<Unit> Handle(RevisionCommand request, CancellationToken cancellationToken)
    {
        var fase = await _context.Intercambios.FirstOrDefaultAsync(x => x.Contrato_Id == request.Id_Contrato);
        
        var contratos = await _context.Contratos
            .Where(x => x.Contrato_Id == request.Id_Contrato)
            .SelectMany(c => c.Instituciones.Users.Select(u => u.Email))
            .ToListAsync(cancellationToken);
        
        
        
        if (fase == null)
        {
            throw new NotFoundException("No existe el contrato");
        }
        
        fase.Revision = !fase.Revision;

        _context.Intercambios.Update(fase);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}