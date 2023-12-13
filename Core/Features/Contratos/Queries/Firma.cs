using Core.Domain.Exceptions;
using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Contratos.Queries;

public record Firma : IRequest<FirmaResponse>
{
    public int Contrato_Id { get; set; } 
}

public class FirmaHandler : IRequestHandler<Firma, FirmaResponse>
{
    private readonly ConvenioContext _context;
    
    public FirmaHandler(ConvenioContext context)
    {
        _context = context;
    }
    
    public async Task<FirmaResponse> Handle(Firma request, CancellationToken cancellationToken)
    {
        var firma = await _context.Intercambios.FirstOrDefaultAsync(x => x.Contrato_Id == request.Contrato_Id);
        
        if (firma == null)
        {
            throw new NotFoundException("No existe el contrato");
        }
        
        var response = new FirmaResponse()
        {
            Firma = firma.Firma
        };

        return response;
    }
}

public record FirmaResponse
{
    public bool Firma { get; set; }
}