namespace Rodriguez.Data.Models
{
    public class ListaCompraProducto
    {
        public int Id { get; set; }

        public int ListaCompraId { get; set; }
        public ListaCompra ListaCompra { get; set; }

        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

        public double Cantidad { get; set; }
    }
}