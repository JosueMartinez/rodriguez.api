namespace rodriguez.api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;
    
    public partial class Producto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Nombre { get; set; }

        public int MedidaId { get; set; }

        public int CategoriaId { get; set; }
        
        public virtual Categoria Categoria { get; set; }
        
        public virtual Medida Medida { get; set; }
    }
}
