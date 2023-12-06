using Core.Domain.Entities;
using Core.Domain.Exceptions;
using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Contratos.Command;

public record FirmaUsuarioCommand : IRequest
{
    public int Contrato_Id { get; set; }
    public bool status { get; set; }
}

public class FirmaUsuarioCommandHandler : IRequestHandler<FirmaUsuarioCommand>
{
    private readonly ConvenioContext _context;
    
    public FirmaUsuarioCommandHandler(ConvenioContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(FirmaUsuarioCommand request, CancellationToken cancellationToken)
    {
        var contrato = await _context.Contratos
            .FirstOrDefaultAsync(x => x.Contrato_Id == request.Contrato_Id);
        
        if (contrato == null)
        {
            throw new NotFoundException("No existe el contrato");
        }

        if (request.status)
        {
            contrato.FechaFinalizacion = DateTime.Now;
            contrato.Status = "Firmado";
        
            _context.Contratos.Update(contrato);
            await _context.SaveChangesAsync(cancellationToken);
        }
        return Unit.Value;
    }
}