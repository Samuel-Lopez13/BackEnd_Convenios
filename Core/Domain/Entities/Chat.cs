using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
    public class Chat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Chat_Id { get; set; }
        public string Mensaje { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public int Contrato_Id { get; set; }
        public int Usuario_Id { get; set; }

        public Contrato Contratos { get; set; } = null!;
        public Usuario Usuarios { get; set; } = null!;
    }
}
