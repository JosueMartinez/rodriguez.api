namespace rodriguez.api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("smrodriguez.moneda")]
    public partial class moneda
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public moneda()
        {
            bonoes = new HashSet<bono>();
            tasas = new HashSet<tasamoneda>();
        }

        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string descripcion { get; set; }

        [Required]
        [StringLength(50)]
        public string simbolo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bono> bonoes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tasamoneda> tasas { get; set; }
    }
}
