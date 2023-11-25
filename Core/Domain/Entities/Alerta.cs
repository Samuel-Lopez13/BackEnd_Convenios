using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Presentation
{
    public class Alerta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Alerta_Id { get; set; }
        public bool? IsAdmin { get; set; }
        public bool? IsInstitucion { get; set; }
        public int? Contrato_Id { get; set; }

        public Contrato? Contratos { get; set; }
    }
}
