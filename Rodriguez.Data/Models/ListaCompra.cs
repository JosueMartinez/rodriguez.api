using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rodriguez.Data.Models
{
    public class ListaCompra
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Nombre { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaUltimaModificacion { get; set; }

        public virtual ICollection<ListaCompraProducto> ProductosLista { get; set; }
    }
}