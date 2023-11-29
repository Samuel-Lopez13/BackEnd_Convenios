using Core.Domain.Services;
using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Instituciones.Queries;

public record InstitucionFull : IRequest<InstitucionFullResponse>;

public class InstitucionFullHandler : IRequestHandler<InstitucionFull, InstitucionFullResponse>
{
    private readonly ConvenioContext _Context;
    private readonly IAuthorization _authorization;
    
    public InstitucionFullHandler(ConvenioContext context, IAuthorization authorization)
    {
        _Context = context;
        _authorization = authorization;
    }
    
    public async Task<InstitucionFullResponse> Handle(InstitucionFull request, CancellationToken cancellationToken)
    {
        var usuario = await _Context.Usuarios
            .Include(u => u.Instituciones)
            .FirstOrDefaultAsync(x => x.Usuario_Id == _authorization.UsuarioActual());

        var completo = false;
        
        if(usuario.Instituciones?.Identificacion == null)
        {
            completo = false;
        }
        else
        {
            completo = true;
        }
            
        var response = new InstitucionFullResponse
        {
            full = completo
        };

        return response;
    }
}

public record InstitucionFullResponse
{
    public bool full { get; set; }
}