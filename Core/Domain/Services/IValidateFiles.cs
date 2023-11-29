using Microsoft.AspNetCore.Http;

namespace Core.Domain.Services;

public interface IValidateFiles
{
    public void ValidateRaw(IFormFile raw);
}