using Core.Domain.Entities;
using Core.Domain.Services;
using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Core.Features.Contratos.Command;

public record CrearContratoCommand : IRequest
{
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public int Institucion_Id { get; set; }
    public IFormFile Archivo { get; set; }
}

public class CrearContratoCommandHandler : IRequestHandler<CrearContratoCommand>
{
    private readonly ConvenioContext _context;
    private readonly IUploadFile _uploadFile;
    
    public CrearContratoCommandHandler(ConvenioContext context, IUploadFile uploadFile)
    {
        _context = context;
        _uploadFile = uploadFile;
    }
    
    public async Task<Unit> Handle(CrearContratoCommand request, CancellationToken cancellationToken)
    {
        var documento = _uploadFile.UploadRaw(request.Archivo, request.Nombre.Trim().ToLower() + ".docx");
        
        var contrato = new Contrato
        {
            Nombre = request.Nombre,
            FechaCreacion = DateTime.Now,
            Descripcion = request.Descripcion,
            Institucion_Id = request.Institucion_Id,
            Status = "Activo",
            File = documento
        };
        
        await _context.Contratos.AddAsync(contrato);
        await _context.SaveChangesAsync();
        
        return Unit.Value;
    }
}