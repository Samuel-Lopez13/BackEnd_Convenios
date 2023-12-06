using System.Net;
using System.Net.Mail;
using Core.Domain.Services;

namespace Core.Features.Usuario.Command;

public class Email : IEmail
{
    public async void EnviarEmail(string correo, string mensaje, string nombre)
    {
        // Configura el correo electrónico y el mensaje
        var email = new MailMessage
        {
            //De quien va dirigido
            From = new MailAddress("pasha.music.inc@gmail.com"),
            //Titulo del mensaje
            Subject = "Contrato",
            //Cuerpo del mensaje
            IsBodyHtml = true,
            Body = $"Contrato: {nombre} {mensaje}"
        };
        //A quien va dirigido
        email.To.Add(correo);

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