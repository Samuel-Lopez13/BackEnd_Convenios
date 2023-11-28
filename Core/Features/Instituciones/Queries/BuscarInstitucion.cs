using MediatR;

namespace Core.Features.Instituciones.Queries;

public record BuscarInstitucion : IRequest<List<BuscarInstitucionResponse>>
{
    public string Nombre { get; set; }
}

public class BuscarInstitucionHandler : IRequestHandler<BuscarInstitucion, List<BuscarInstitucionResponse>>
{
    public Task<List<BuscarInstitucionResponse>> Handle(BuscarInstitucion request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public record BuscarInstitucionResponse
{
    public int Institucion_Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Ciudad { get; set; }
    public string? Estado { get; set; }
    public string? Pais { get; set; }
    public string? Identificacion { get; set; }
    public string? Direccion { get; set; }
}