using Core.Domain.Services;
using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Chat.Command;

public record ChatCommand : IRequest
{
    public string Mensaje { get; set; } = null!;
    public int Id_Contrato { get; set; }
}

public class ChatCommandHandler : IRequestHandler<ChatCommand>
{
    private readonly ConvenioContext _context;
    private readonly IAuthorization _authorization;
    
    public ChatCommandHandler(ConvenioContext context, IAuthorization authorization)
    {
        _context = context;
        _authorization = authorization;
    }
    
    public async Task<Unit> Handle(ChatCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Usuario_Id == _authorization.UsuarioActual());
        
        var chat = new Domain.Entities.Chat()
        {
            Mensaje = request.Mensaje,
            Fecha = DateTime.Now,
            Contrato_Id = request.Id_Contrato,
            Usuario_Id = usuario.Usuario_Id
        };
        
        await _context.Chats.AddAsync(chat, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}