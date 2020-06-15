using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rodriguez.Data.Models
{
    public class Bono
    {
        public int Id { get; set; }

        public double Monto { get; set; }

        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }

        public int TasaId { get; set; }
        public TasaMoneda Tasa { get; set; }

        public string PayPalId { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreDestino { get; set; }

        [Required]
        [StringLength(50)]
        public string ApellidoDestino { get; set; }

        [Required]
        [StringLength(11)]
        public string CedulaDestino { get; set; }

        [Required]
        [StringLength(10)]
        public string TelefonoDestino { get; set; }

        public int EstadoBonoId { get; set; }
        public EstadoBono EstadoBono { get; set; }

        public DateTime FechaCompra { get; set; }

        public virtual ICollection<HistorialBono> HistorialBonos { get; set; }
    }
}