namespace rodriguez.api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("smrodriguez.bono")]
    public partial class bono
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public bono()
        {
            historial_bono = new HashSet<historial_bono>();
        }

        public int id { get; set; }

        public double monto { get; set; }

        public int moneda { get; set; }

        public int remitente { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre_destino { get; set; }

        [Required]
        [StringLength(50)]
        public string apellido_destino { get; set; }

        [Required]
        [StringLength(11)]
        public string cedula_destino { get; set; }

        public int estado { get; set; }

        public DateTime fechaCompra { get; set; }

        public virtual cliente cliente { get; set; }

        public virtual moneda moneda1 { get; set; }

        public virtual estado_bono estado_bono { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<historial_bono> historial_bono { get; set; }
    }
}
