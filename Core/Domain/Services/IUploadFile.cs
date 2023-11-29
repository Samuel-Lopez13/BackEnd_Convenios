using Microsoft.AspNetCore.Http;

namespace Core.Domain.Services;

public interface IUploadFile
{
    public string UploadRaw(IFormFile raw, string titulo);
}