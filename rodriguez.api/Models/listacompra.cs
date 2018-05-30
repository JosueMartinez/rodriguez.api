namespace rodriguez.api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    
    public partial class ListaCompra
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ListaCompra()
        {
            Productos = new HashSet<ListaProducto>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Nombre { get; set; }

        public int ClienteId { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaUltimaModificacion { get; set; }

        public virtual Cliente Cliente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ListaProducto> Productos { get; set; }
    }
}
