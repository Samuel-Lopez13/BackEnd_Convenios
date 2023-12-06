using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Contratos.Queries;

public record Revision : IRequest<RevisionResponse>
{
    public int Contrato_Id { get; set; } 
}

public class RevisionHandler : IRequestHandler<Revision, RevisionResponse>
{
    private readonly ConvenioContext _context;
    
    public RevisionHandler(ConvenioContext context)
    {
        _context = context;
    }
    
    public async Task<RevisionResponse> Handle(Revision request, CancellationToken cancellationToken)
    {
        var fase = await _context.Intercambios.FirstOrDefaultAsync(x => x.Contrato_Id == request.Contrato_Id);

        var response = new RevisionResponse()
        {
            Revision = fase.Revision
        };

        return response;
    }
}

public record RevisionResponse
{
    public bool Revision { get; set; }
}