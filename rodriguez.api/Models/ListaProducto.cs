namespace rodriguez.api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    
    public partial class ListaProducto
    {
        public int Id { get; set; }

        public int ListaCompraId { get; set; }

        public int ProductoId { get; set; }

        public int Cantidad { get; set; }

        public virtual ListaCompra Lista { get; set; }

        public virtual Producto Producto { get; set; }
    }
}
