using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rodriguez.Data.Models
{
    public class Medida
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Descripcion { get; set; }

        [Required]
        [StringLength(4)]
        public string Simbolo { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}