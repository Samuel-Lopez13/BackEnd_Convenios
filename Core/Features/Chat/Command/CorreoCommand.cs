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
                        .contenedor{
                            width: 100%;
                            background-color: #F2F2F2;
                            box-sizing: border-box;
                            padding-left: calc((100% - 600px) / 2);
                        }

                        .contenido{
                            width: 600px;
                            height: 100%;
                            background-color: #F5F5F5;
                            display: flex;
                            flex-direction: column;
                        }

                        header{
                            width: 100%;
                            height: 130px;
                            background-color: #1B355D;
                            display: flex;
                        }

                        .uac{
                            width: 50%;
                            height: 100%;
                            display: flex;
                            align-items: center;
                        }

                        .uac_img{
                            width: 200px;
                        }

                        .convenios{
                            width: 50%;
                            height: 100%;
                            display: flex;
                            justify-content: end;
                            padding: 15px;
                        }

                        .convenios_img{
                            width: 120px;
                            height: 55px;
                        }

                        .encabezado{
                            width: 100%;
                            height: 50px;
                            background-color: #BDBDBD;
                            display: flex;
                            align-items: center;
                            justify-content: center;
                        }

                        .encabezado__frase{
                            font-size: 13px;
                            font-family: Arial, Helvetica, sans-serif;
                            margin: 0;
                        }
                    </style>
                </head>
                <body>
                    <div class='contenedor'>
                        <div class='contenido'>
                            <header>
                                <div class='uac'>
                                    <img class='uac_img' src='https://github.com/Samuel-Lopez13/FrontEnd_Convenios/blob/main/src/assets/imagenes/UAC.png?raw=true' alt='logo'>
                                </div>
                                <div class='convenios'>
                                    <img class='convenios_img' src='https://github.com/Samuel-Lopez13/FrontEnd_Convenios/blob/main/src/assets/imagenes/LogoConvenios.png?raw=true' alt='usuario'>
                                </div>
                            </header>
                            <div class='encabezado'>
                                <h5 class='encabezado__frase'>
                                    Alerta se ha realizado una revision de la contraparte
                                </h5>
                            </div>
                        </div>
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