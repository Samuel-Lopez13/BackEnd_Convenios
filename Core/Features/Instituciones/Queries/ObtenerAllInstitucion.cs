using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Instituciones.Queries;

public record ObtenerAllInstitucion : IRequest<List<ObtenerAllInstitucionResponse>>;

public class ObtenerrAllInstitucionHandler : IRequestHandler<ObtenerAllInstitucion, List<ObtenerAllInstitucionResponse>>
{
    private readonly ConvenioContext _context;

    public ObtenerrAllInstitucionHandler(ConvenioContext context)
    {
        _context = context;
    }

    public async Task<List<ObtenerAllInstitucionResponse>> Handle(ObtenerAllInstitucion request, CancellationToken cancellationToken)
    {
        var instituciones = await _context.Instituciones.ToListAsync();

        var response = instituciones.Select(x => new ObtenerAllInstitucionResponse()
        {
            Institucion_Id = x.Institucion_Id,
            Nombre = x.Nombre
        }).ToList();

        return response;
    }
}

public record ObtenerAllInstitucionResponse
{
    public int Institucion_Id { get; set; }
    public string Nombre { get; set; }
}