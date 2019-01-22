using System;
using System.ComponentModel.DataAnnotations;

namespace Rodriguez.Data.Models
{
    public class TasaMoneda
    {
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