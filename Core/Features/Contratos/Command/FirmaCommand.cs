using Core.Domain.Exceptions;
using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Contratos.Command;

public record FirmaCommand : IRequest
{
    public int Id_Contrato { get; set; }
}

public class FirmaCommandHandler : IRequestHandler<FirmaCommand>
{
    private readonly ConvenioContext _context;
    
    public FirmaCommandHandler(ConvenioContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(FirmaCommand request, CancellationToken cancellationToken)
    {
        var firma = await _context.Intercambios.FirstOrDefaultAsync(x => x.Contrato_Id == request.Id_Contrato);

        if (firma == null)
        {
            throw new NotFoundException("No existe el contrato");
        }
        
        firma.Firma = !firma.Firma;

        _context.Intercambios.Update(firma);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}