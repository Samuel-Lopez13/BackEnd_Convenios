using System.ComponentModel.DataAnnotations;
using System.Text;
using Core.Domain.Exceptions;
using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Usuarios.Command;

public record CrearUsuarioCommand : IRequest<CrearUsuarioResponse>
{
    [Required]
    public string Nombre { get; set; }
    [Required]
    public string Correo { get; set; }
    [Required]
    public int Institucion_Id { get; set; }
}

public class CrearUsuarioCommandHandler : IRequestHandler<CrearUsuarioCommand, CrearUsuarioResponse>
{
    private readonly ConvenioContext _context;
    
    public CrearUsuarioCommandHandler(ConvenioContext context)
    {
        _context = context;
    }
    
    public async Task<CrearUsuarioResponse> Handle(CrearUsuarioCommand request, CancellationToken cancellationToken)
    {
        var validar = await _context.Usuarios.FirstOrDefaultAsync(x => x.Email == request.Correo);

        if (validar != null)
        {
            throw new BadRequestException("Error ya existe un usuario con ese correo");
        }
        
        var usuario = new Domain.Entities.Usuario
        {
            Nombre = request.Nombre,
            Email = request.Correo,
            Institucion_Id = request.Institucion_Id,
            AcceptTerms = false,
            Rol_Id = 2,
            Password = GenerarClave()
        };
            
        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();
        
        var response = new CrearUsuarioResponse
        {
            Correo = usuario.Email,
            Contrasena = usuario.Password
        };
        
        return response;
    }
    
    private string GenerarClave()
    {
        // Genera un token aleatorio simple
        var random = new Random();
        const string caracteresPermitidos = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        var token = new StringBuilder();

        // Genera un token de 6 caracteres
        for (int i = 0; i < 8; i++)
        {
            token.Append(caracteresPermitidos[random.Next(caracteresPermitidos.Length)]);
        }

        return token.ToString();
    }
}

public record CrearUsuarioResponse
{
    public string Correo { get; set; }
    public string Contrasena { get; set; }
}