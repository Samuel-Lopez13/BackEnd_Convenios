using System.Net;
using GroupDocs.Comparison;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Core.Features.Contratos.Queries;

public record Verificacion : IRequest
{
    public IFormFile Documento { get; set; }
    public IFormFile Revision { get; set; }
}

public class VerificacionResponse : IRequestHandler<Verificacion>
{
    public async Task<Unit> Handle(Verificacion request, CancellationToken cancellationToken)
    {
        // URL de Cloudinary
        string url1 = "https://res.cloudinary.com/doi0znv2t/raw/upload/v1702243621/irjiuplbw1fznq9i3pfv.docx";
        string url2 = "https://res.cloudinary.com/doi0znv2t/raw/upload/v1702243621/hl8t54rtloe0hwmfofhx.docx";
        string url3 = "https://res.cloudinary.com/doi0znv2t/raw/upload/v1702243621/wlm9ewxwzhq2tnpsvypb.docx";

        // Descargar archivos desde Cloudinary
        string localPath1 = DownloadFile(url1);
        string localPath2 = DownloadFile(url2);
        string localPath3 = DownloadFile(url3);

        // Realizar la comparación
        using (Comparer comparer = new Comparer(localPath1))
        {
            comparer.Add(localPath2);
            comparer.Compare(localPath3);
        }
        
        return Unit.Value;
    }
    
    // Método para descargar un archivo desde una URL y devolver la ruta local
    private static string DownloadFile(string url)
    {
        string localPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

        using (WebClient client = new WebClient())
        {
            client.DownloadFile(url, localPath);
        }
        
        Console.WriteLine("Archivo descargado en: " + localPath);

        return localPath;
    }
}