namespace rodriguez.api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("smrodriguez.categoria")]
    public partial class categoria
    {
        public int id { get; set; }

        [Required]
        [StringLength(30)]
        public string descripcion { get; set; }
    }
}
