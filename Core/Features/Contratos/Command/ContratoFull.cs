using Core.Domain.Exceptions;
using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Contratos.Command;

public record ContratoFull : IRequest<ContratoFullResponse>
{
    public int Contrato_Id { get; set; }
}

public class ContratoFullHandler : IRequestHandler<ContratoFull, ContratoFullResponse>
{
    private readonly ConvenioContext _context;
    
    public ContratoFullHandler(ConvenioContext context)
    {
        _context = context;
    }
    
    public async Task<ContratoFullResponse> Handle(ContratoFull request, CancellationToken cancellationToken)
    {
        var contrato = await _context.Contratos.FirstOrDefaultAsync(x => x.Contrato_Id == request.Contrato_Id);

        if (contrato == null)
        {
            throw new NotFoundException("No existe el contrato");
        }

        bool listo = false;

        if(contrato.Status == "Activo")
            listo = true;
        
        if (contrato.FechaFinalizacion != null)
            listo = true;
        
        var response = new ContratoFullResponse()
        {
            Listo = listo
        };

        return response;
        
    }
}

public record ContratoFullResponse
{
    public bool Listo { get; set; }
}