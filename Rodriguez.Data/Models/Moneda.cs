using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rodriguez.Data.Models
{
    public class Moneda
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }

        [Required]
        [StringLength(50)]
        public string Simbolo { get; set; }

        public virtual ICollection<TasaMoneda> Tasas { get; set; }
    }
}