namespace Core.Domain.Services;

public interface IEmail
{
    void EnviarEmail(string correo, string mensaje, string nombre);
}