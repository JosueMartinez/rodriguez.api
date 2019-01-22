using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rodriguez.Data.Models
{
    public class EstadoBono
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }

        public virtual ICollection<Bono> Bonos { get; set; }
        public virtual ICollection<HistorialBono> HistorialBonos { get; set; }
        
    }
}