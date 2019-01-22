using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rodriguez.Data.Models
{
    public class Rol
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Descripcion { get; set; }

        public virtual ICollection<Usuario> usuarios { get; set; }
    }
}