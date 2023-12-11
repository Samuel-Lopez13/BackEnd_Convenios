using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
    public class Usuario
    {
        public Usuario()
        {
            Chats = new HashSet<Chat>();
            Logs = new HashSet<Log>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Usuario_Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public bool? AcceptTerms { get; set; }
        public int? Rol_Id { get; set; }
        public int? Institucion_Id { get; set; }

        public Institucion? Instituciones { get; set; } = null!;
        public virtual Rol? Roles { get; set; }
        
        public virtual ICollection<Chat> Chats { get; set; }
        public virtual ICollection<Log> Logs { get; set; }
    }
}
