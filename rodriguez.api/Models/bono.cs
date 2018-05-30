namespace rodriguez.api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Bono
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bono()
        {
            Historial = new HashSet<HistorialBono>();
        }

        public int Id { get; set; }

        public double Monto { get; set; }

        //public int monedaId { get; set; }

        public int ClienteId { get; set; }

        public int TasaId { get; set; }

        public string PaypalId { get; set; }

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

        public DateTime FechaCompra { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual EstadoBono Estado { get; set; }

        public virtual Tasa Tasa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HistorialBono> Historial { get; set; }
    }
}
