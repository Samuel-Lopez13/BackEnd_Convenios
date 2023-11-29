using Core.Domain.Entities;
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
    
    public CrearContratoCommandHandler(ConvenioContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(CrearContratoCommand request, CancellationToken cancellationToken)
    {
        var contrato = new Contrato
        {
            Nombre = request.Nombre,
            FechaCreacion = DateTime.Now,
            Descripcion = request.Descripcion,
            Institucion_Id = request.Institucion_Id,
            Status = "Activo"
        };
        
        return Unit.Value;
    }
}