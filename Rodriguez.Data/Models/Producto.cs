using System.ComponentModel.DataAnnotations;

namespace Rodriguez.Data.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Nombre { get; set; }

        public int MedidaId { get; set; }
        public Medida Medida { get; set; }

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }
}