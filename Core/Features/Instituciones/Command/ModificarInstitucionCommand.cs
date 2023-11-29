using Core.Domain.Services;
using Core.Infraestructure.Persistance;
using MediatR;

namespace Core.Features.Instituciones.Command;

public record ModificarInstitucionCommand : IRequest
{
    public string Pais { get; set; }
    public string Estado { get; set; }
    public string Ciudad { get; set; }
    public string Identificacion { get; set; }
    public string Direccion { get; set; }
}

public class ModificarInstitucionHandler : IRequestHandler<ModificarInstitucionCommand>
{
    private readonly ConvenioContext _context;
    private readonly IAuthorization _authorization;
    
    public ModificarInstitucionHandler(ConvenioContext context, IAuthorization authorization)
    {
        _context = context;
        _authorization = authorization;
    }
    
    public async Task<Unit> Handle(ModificarInstitucionCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}