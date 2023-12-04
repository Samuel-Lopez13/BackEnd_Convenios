namespace Core.Features.Contratos.Queries;

public record ContratosId
{
    public int Contrato_Id { get; set; } 
}

public record ContratoIdResponse
{
    public string File { get; set; } = null!;
}