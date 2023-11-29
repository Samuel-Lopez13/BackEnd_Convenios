using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Instituciones.Queries;

public record BuscarAllInstitucion : IRequest<List<BuscarAllInstitucionResponse>>
{
    public string Nombre { get; set; }
}

public class BuscarAllInstitucionHandler : IRequestHandler<BuscarAllInstitucion, List<BuscarAllInstitucionResponse>>
{
    private readonly ConvenioContext _context;

    public BuscarAllInstitucionHandler(ConvenioContext context)
    {
        _context = context;
    }

    public async Task<List<BuscarAllInstitucionResponse>> Handle(BuscarAllInstitucion request, CancellationToken cancellationToken)
    {
        var instituciones = await _context.Instituciones
            .Where(x => x.Nombre.Contains(request.Nombre))
            .ToListAsync();

        var response = instituciones.Select(x => new BuscarAllInstitucionResponse()
        {
            Institucion_Id = x.Institucion_Id,
            Nombre = x.Nombre
        }).ToList();

        return response;
    }
}

public record BuscarAllInstitucionResponse
{
    public int Institucion_Id { get; set; }
    public string Nombre { get; set; }
}