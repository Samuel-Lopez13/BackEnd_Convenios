using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Presentation
{
    public class Institucion
    {
        public Institucion()
        {
            Agreements = new HashSet<Contrato>();
            Users = new HashSet<Usuario>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Institucion_Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Ciudad { get; set; }
        public string? Estado { get; set; }
        public string? Pais { get; set; }
        public string? Identificacion { get; set; }
        public string? Direccion { get; set; }

        public virtual ICollection<Contrato> Agreements { get; set; }
        public virtual ICollection<Usuario> Users { get; set; }
    }
}
