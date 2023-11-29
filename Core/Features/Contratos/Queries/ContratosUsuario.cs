using Core.Domain.Services;
using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Contratos.Queries;

public record ContratosUsuario : IRequest<List<ContratosUsuariosResponse>>;

public class ContratosUsuariosHandler : IRequestHandler<ContratosUsuario, List<ContratosUsuariosResponse>>
{
    private readonly ConvenioContext _context;
    private readonly IAuthorization _authorization;
    
    public ContratosUsuariosHandler(ConvenioContext context, IAuthorization authorization)
    {
        _context = context;
        _authorization = authorization;
    }

    public async Task<List<ContratosUsuariosResponse>> Handle(ContratosUsuario request, CancellationToken cancellationToken)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Usuario_Id == _authorization.UsuarioActual());
        
        var contratos = await _context.Contratos
            .Where(x => x.Institucion_Id == usuario.Institucion_Id)
            .Select(x => new ContratosUsuariosResponse
            {
                Contratos_Id = x.Contrato_Id,
                Nombre = x.Nombre
            })
            .ToListAsync(cancellationToken);

        return contratos;
    }
}

public record ContratosUsuariosResponse
{
    public int Contratos_Id { get; set; }
    public string Nombre { get; set; }
}