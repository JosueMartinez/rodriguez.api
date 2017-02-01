namespace rodriguez.api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("smrodriguez.lista_compra")]
    public partial class lista_compra
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public lista_compra()
        {
            lista_compra_producto = new HashSet<lista_compra_producto>();
        }

        public int id { get; set; }

        [StringLength(50)]
        public string nombre { get; set; }

        public int creador { get; set; }

        public DateTime fecha_creacion { get; set; }

        public DateTime? fecha_ultima_modificacion { get; set; }

        public bool activa { get; set; }

        public virtual cliente cliente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<lista_compra_producto> lista_compra_producto { get; set; }
    }
}
