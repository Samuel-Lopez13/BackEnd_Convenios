using System.Net;
using System.Net.Mail;
using System.Text;
using Core.Domain.Exceptions;
using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Chat.Command;

public record CorreoCommand : IRequest
{
    public string Correo { get; set; } = null!;
}

public class MensajeCommandHandler : IRequestHandler<CorreoCommand>
{
    private readonly ConvenioContext _context;

    public MensajeCommandHandler(ConvenioContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(CorreoCommand request, CancellationToken cancellationToken)
    {
        // Buscar usuarios con el correo proporcionado
        var correo = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == request.Correo);

        //Si no existe el correo enviara un 404
        if (correo == null)
        {
            throw new NotFoundException($"No se encontro el correo {correo.Email}");
        }

        // Genera un token de verificación
        string tokenGenerado = GenerarToken();

        // Enviar el correo electrónico con el token
        EnviarCorreoVerificacion(request.Correo, tokenGenerado);

        return Unit.Value;
    }
    
    private string GenerarToken()
    {
        // Genera un token aleatorio simple
        var random = new Random();
        const string caracteresPermitidos = "0123456789";
        var token = new StringBuilder();

        // Genera un token de 6 caracteres
        for (int i = 0; i < 6; i++)
        {
            token.Append(caracteresPermitidos[random.Next(caracteresPermitidos.Length)]);
        }

        return token.ToString();
    }

    private string mensaje()
    {
        return @"
                <html>
                <head>
                    <style>
                        body {
                            text-align: center;
                            font-family: Arial, sans-serif;
                        }
                        .container {
                            background-color: #f2f2f2;
                            width: 100%;
                            padding: 0 30%;
                        }

                        header {
                            width: 40%;
                            background-color: #3498db;
                            text-align: center;
                            font-size: 35px;
                            color: black;
                        }
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <header>
                            <h1>Firma de convenios</h1>
                        </header>
                    </div>
                </body>
                </html>";
    }
    
    private async Task EnviarCorreoVerificacion(string correoElectronico, string token)
    {
        // Configura el correo electrónico y el mensaje
        var email = new MailMessage
        {
            //De quien va dirigido
            From = new MailAddress("pasha.music.inc@gmail.com"),
            //Titulo del mensaje
            Subject = "Recuperación de contraseña",
            //Cuerpo del mensaje
            IsBodyHtml = true,
            Body = mensaje() 
        };
        //A quien va dirigido
        email.To.Add(correoElectronico);

        // Configura el cliente SMTP
        var smtpClient = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            Credentials =
                //Desde gmail "Contrasenñas de aplicaciones" generas el password
                new NetworkCredential("pasha.music.inc@gmail.com","lhdx uwcz bhva rqdt"),
            EnableSsl = true
        };

        // Envía el correo electrónico
        await smtpClient.SendMailAsync(email);
    }
}