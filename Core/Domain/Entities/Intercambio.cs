using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities;

public class Intercambio
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Intercambio_Id { get; set; }
    
    public bool Revision { get; set; }
    public bool Firma { get; set; }
     
    public int Contrato_Id { get; set; }
    
    public Contrato Contratos { get; set; } = null!;
}