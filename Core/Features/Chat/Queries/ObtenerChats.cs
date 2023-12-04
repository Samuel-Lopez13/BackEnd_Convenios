using Core.Domain.Services;
using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Core.Features.Chat.Queries;

public record ObtenerChats : IRequest<List<ObtenerChatsResponse>>
{
    public int Id_Contrato { get; set; }
}

public class ObtenerChatsHandler : IRequestHandler<ObtenerChats, List<ObtenerChatsResponse>>
{
    private readonly ConvenioContext _context;
    private readonly IAuthorization _authorization;
    
    public ObtenerChatsHandler(ConvenioContext context, IAuthorization authorization)
    {
        _context = context;
        _authorization = authorization;
    }
    
    public async Task<List<ObtenerChatsResponse>> Handle(ObtenerChats request, CancellationToken cancellationToken)
    {
        var chats = await _context.Chats
            .AsNoTracking()
            .Include(u => u.Usuarios)
            .Where(c => c.Contrato_Id == request.Id_Contrato)
            .OrderBy(c => c.Fecha)
            .ToListAsync();
        
        var response = chats.Select(x => new ObtenerChatsResponse{
            Mensaje = x.Mensaje,
            IsMe = x.Usuarios?.Usuario_Id == _authorization.UsuarioActual(),
            Usuario = x.Usuarios?.Nombre ?? "Sin nombre"
        }).ToList();

        return response;
    }
}

public record ObtenerChatsResponse
{
    public string Mensaje { get; set; } = null!;
    public bool IsMe { get; set; }
    public string Usuario { get; set; } = null!;
}