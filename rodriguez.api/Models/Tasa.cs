using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace rodriguez.api.Models
{
    public partial class Tasa
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public int MonedaId { get; set; }

        [Required]
        public double Valor { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public bool Activa { get; set; }

        public virtual Moneda Moneda { get; set; }
    }
}