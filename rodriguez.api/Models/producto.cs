namespace rodriguez.api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("smrodriguez.producto")]
    public partial class producto
    {
        public int id { get; set; }

        [Required]
        [StringLength(30)]
        public string nombre { get; set; }

        [StringLength(20)]
        public string medida { get; set; }
    }
}
