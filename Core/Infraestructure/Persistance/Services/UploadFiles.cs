using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.Domain.Services;
using Microsoft.AspNetCore.Http;

namespace Core.Features.Usuario.Command;

public class UploadFiles : IUploadFile
{
    private readonly IConvertToBase64 _convert;
    private readonly Account account;
    private readonly Cloudinary cloudinary;
    private readonly IValidateFiles _validateFiles;
    
    public UploadFiles(IConvertToBase64 convert, IValidateFiles validateFiles)
    {
        _convert = convert;
        _validateFiles = validateFiles;
        account = new Account("dnbwjuwvx", "827831474231854", "MlHgPWXseizcd_NGAGW6HV9zZtE");
        cloudinary = new Cloudinary(account);
    }

    public string UploadRaw(IFormFile raw, string titulo)
    {
        //_validateFiles.ValidateRaw(raw);
        
        //tipo de formato que se subira
        RawUploadResult result = cloudinary.UploadLarge(new RawUploadParams
        {
            //Sube una imagen en base64 y lo desconvierte para subirla
            File = new FileDescription(Guid.NewGuid().ToString(), new MemoryStream(Convert.FromBase64String(_convert.ConvertBase64(raw)))),PublicId = titulo
        });
        
        return result.Url.ToString();
    }
}