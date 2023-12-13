using Core.Domain.Exceptions;
using Core.Infraestructure.Persistance;
using MediatR;

namespace Core.Features.Contratos.Queries;

public record Status : IRequest<StatusResponse>
{
    public int Contrato_Id { get; set; }
}

public class StatusHandler : IRequestHandler<Status, StatusResponse>
{
    private readonly ConvenioContext _context;
    
    public StatusHandler(ConvenioContext context)
    {
        _context = context;
    }
    
    public async Task<StatusResponse> Handle(Status request, CancellationToken cancellationToken)
    {
        var contrato = await _context.Contratos.FindAsync(request.Contrato_Id);
        
        if (contrato == null)
        {
            throw new NotFoundException("El contrato no existe");
        }
        
        return new StatusResponse
        {
            status = contrato.Status
        };
    }
}

public record StatusResponse
{
    public string status { get; set; }
}