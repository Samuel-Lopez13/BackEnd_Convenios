using System.ComponentModel.DataAnnotations;
using Core.Domain.Exceptions;
using Core.Domain.Services;
using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Usuarios.Command;

public record LoginCommand : IRequest<LoginCommandResponse>
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Contrasena { get; set; }
}


public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
{
    private readonly IAuthService _authService;
    private readonly ConvenioContext _context;
    
    public LoginCommandHandler(IAuthService authService, ConvenioContext context)
    {
        _authService = authService;
        _context = context;
    }
    
    public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        //Valida que no esten vacias
        if(string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Contrasena))
            throw new BadRequestException("Correo y contraseña son obligatorios");
        
        //Si cumple con las validaciones se procede a autenticar
        var token = await _authService.AuthenticateAsync(request.Email, request.Contrasena);
        
        if(token == null)
            throw new NotFoundException("Usuario no encontrado");
        
        var usuario = await _context.Usuarios
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(x => x.Email == request.Email);
        
        //Respuesta
        return new LoginCommandResponse()
        {
            Token = token,
            Rol = usuario?.Roles.Nombre ?? "Sin Rol"
        };
    }
}

public record LoginCommandResponse
{
    public string Token { get; set; }
    public string Rol { get; set; }
}