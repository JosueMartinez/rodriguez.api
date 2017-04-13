namespace rodriguez.api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("smrodriguez.listacompraproducto")]
    public partial class listacompraproducto
    {
        public int id { get; set; }

        public int listaCompraId { get; set; }

        public int productoId { get; set; }

        public int cantidad { get; set; }

        public virtual listacompra listacompra { get; set; }

        public virtual producto producto { get; set; }
    }
}
