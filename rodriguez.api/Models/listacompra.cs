namespace rodriguez.api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("smrodriguez.listacompra")]
    public partial class listacompra
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public listacompra()
        {
            listacompraproductoes = new HashSet<listacompraproducto>();
        }

        public int id { get; set; }

        [StringLength(50)]
        public string nombre { get; set; }

        public int clienteId { get; set; }

        public DateTime fechaCreacion { get; set; }

        public DateTime? fechaUltimaModificacion { get; set; }

        public virtual cliente cliente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<listacompraproducto> listacompraproductoes { get; set; }
    }
}
