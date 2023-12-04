using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
    public class Contrato
    {
        public Contrato()
        {
            Alerts = new HashSet<Alerta>();
            Chats = new HashSet<Chat>();
            Logs = new HashSet<Log>();
            Intercambios = new HashSet<Intercambio>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Contrato_Id { get; set; }
        public string Nombre { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaFinalizacion { get; set; }
        public string? Descripcion { get; set; }
        public string? Status { get; set; }
        public string? File { get; set; }
        public string Fase { get; set; }
        public int Institucion_Id { get; set; }

        public Institucion Instituciones { get; set; } = null!;
        
        public virtual ICollection<Alerta> Alerts { get; set; }
        public virtual ICollection<Chat> Chats { get; set; }
        public virtual ICollection<Log> Logs { get; set; }
        public virtual ICollection<Intercambio> Intercambios { get; set; }
    }
}
