namespace rodriguez.api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("lista_compra_producto")]
    public partial class lista_compra_producto
    {
        public int id { get; set; }

        public int lista { get; set; }

        public int producto { get; set; }

        public int cantidad { get; set; }

        public virtual lista_compra lista_compra { get; set; }
    }
}
