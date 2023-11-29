using Core.Infraestructure.Persistance;
using MediatR;

namespace Core.Features.Contratos.Command;

public record EliminarContratoCommand : IRequest
{
    public int Contrato_Id { get; set; }
}

public class EliminarContratoCommandHandler : IRequestHandler<EliminarContratoCommand>
{
    private readonly ConvenioContext _context;
    
    public EliminarContratoCommandHandler(ConvenioContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(EliminarContratoCommand request, CancellationToken cancellationToken)
    {
        var contrato = await _context.Contratos.FindAsync(request.Contrato_Id);
        
        if(contrato == null)
            throw new Exception("Contrato no encontrado");
        
        _context.Contratos.Remove(contrato);
        await _context.SaveChangesAsync();

        return Unit.Value;
    }
}