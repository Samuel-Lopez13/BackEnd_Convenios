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
    private readonly IEmail _email;
    
    public ModificarContratosIdHandler(ConvenioContext context, IUploadFile uploadFile, IEmail email)
    {
        _context = context;
        _uploadFile = uploadFile;
        _email = email;
    }
    
    public async Task<Unit> Handle(ModifcarContratosId request, CancellationToken cancellationToken)
    {
        var contratos = await _context.Contratos.FirstOrDefaultAsync(x => x.Contrato_Id == request.Contrato_Id);
        
        var fase = await _context.Intercambios.FirstOrDefaultAsync(x => x.Contrato_Id == request.Contrato_Id);

        if (fase == null)
        {
            throw new NotFoundException("No existe el contrato");
        }
        
        if (contratos == null)
        {
            throw new NotFoundException("No se encontro el contrato");
        }
        
        var documento = _uploadFile.UploadRaw(request.File, contratos.Nombre.Trim().ToLower() + "C.docx");
        
        //Modificamos el contrato
        contratos.FileAntiguo = contratos.File;
        contratos.File = documento;
        
        //Cambiamos el estado de la revision
        fase.Revision = !fase.Revision;

        //Actualizamos los datos
        _context.Intercambios.Update(fase);
        _context.Contratos.Update(contratos);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        var contrat = await _context.Contratos
            .Where(x => x.Contrato_Id == request.Contrato_Id)
            /*.Select(c => new
            {
                ContratoNombre = c.Nombre, // Ajusta esto según la propiedad real de tu contrato
                UsuariosEmails = c.Instituciones.Users.Select(u => u.Email).ToList()
            })*/
            .SelectMany(c => c.Instituciones.Users.Select(u => u.Email))
            .ToListAsync(cancellationToken);
        
        foreach (var usuario in contrat)
        {
            _email.EnviarEmail(usuario, contratos.Nombre, "se mando ha revision");
        }
        
        return Unit.Value;
    }
}
