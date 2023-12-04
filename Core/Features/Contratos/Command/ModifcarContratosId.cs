using Core.Domain.Exceptions;
using Core.Domain.Services;
using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Contratos.Command;

public record ModifcarContratosId : IRequest
{
    public int Contrato_Id { get; set; }
    public IFormFile File { get; set; }
}

public class ModificarContratosIdHandler : IRequestHandler<ModifcarContratosId>
{
    private readonly ConvenioContext _context;
    private readonly IUploadFile _uploadFile;
    
    public ModificarContratosIdHandler(ConvenioContext context, IUploadFile uploadFile)
    {
        _context = context;
        _uploadFile = uploadFile;
    }
    
    public async Task<Unit> Handle(ModifcarContratosId request, CancellationToken cancellationToken)
    {
        var contratos = await _context.Contratos.FirstOrDefaultAsync(x => x.Contrato_Id == request.Contrato_Id);
        
        if (contratos == null)
        {
            throw new NotFoundException("No se encontro el contrato");
        }
        
        var documento = _uploadFile.UploadRaw(request.File, contratos.Nombre.Trim().ToLower() + "C.docx");
        
        contratos.FileAntiguo = contratos.File;
        contratos.File = documento;
        
        _context.Contratos.Update(contratos);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}
