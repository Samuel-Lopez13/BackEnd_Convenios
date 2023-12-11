using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
    public class Log
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Log_Id { get; set; }
        
        //Donde se vera la alerta sera una lista de notificaciones
        public string Action { get; set; } = null!;
        public DateTime? Fecha { get; set; }
        public int? Usuario_Id { get; set; }
        public int? Contrato_Id { get; set; }

        public virtual Contrato? Contratos { get; set; }
        public virtual Usuario? Usuarios { get; set; } = null!;
    }
}
