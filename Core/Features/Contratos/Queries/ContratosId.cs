using Core.Domain.Exceptions;
using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Contratos.Queries;

public record ContratosId : IRequest<ContratoIdResponse>
{
    public int Contrato_Id { get; set; }
}

public class ContratosIdHandler : IRequestHandler<ContratosId, ContratoIdResponse>
{
    private readonly ConvenioContext _context;
    
    public ContratosIdHandler(ConvenioContext context)
    {
        _context = context;
    }
    
    public async Task<ContratoIdResponse> Handle(ContratosId request, CancellationToken cancellationToken)
    {
        var contratos = await _context.Contratos.FirstOrDefaultAsync(x => x.Contrato_Id == request.Contrato_Id);
        
        if (contratos == null)
        {
            throw new NotFoundException("No se encontro el contrato");
        }

        var response = new ContratoIdResponse
        {
            File = contratos.File,
            Nombre = contratos.Nombre
        };
        
        return response;
    }
}

public record ContratoIdResponse
{
    public string File { get; set; } = null!;
    public string Nombre { get; set; } = null!;
}