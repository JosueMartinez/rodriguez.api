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
            historialbonoes = new HashSet<historialbono>();
        }

        public int id { get; set; }

        public double monto { get; set; }

        //public int monedaId { get; set; }

        public int clienteId { get; set; }

        public int tasaId { get; set; }

        [Required]
        [StringLength(50)]
        public string nombreDestino { get; set; }

        [Required]
        [StringLength(50)]
        public string apellidoDestino { get; set; }

        [Required]
        [StringLength(11)]
        public string cedulaDestino { get; set; }

        [Required]
        [StringLength(10)]
        public string telefonoDestino { get; set; }

        public int estadoBonoId { get; set; }

        public DateTime fechaCompra { get; set; }

        public virtual cliente cliente { get; set; }

        //public virtual moneda moneda { get; set; }

        public virtual estadobono estadobono { get; set; }

        public virtual tasamoneda tasa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<historialbono> historialbonoes { get; set; }
    }
}
