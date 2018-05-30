using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using rodriguez.api.Models;

namespace rodriguez.api.Clases
{
    public static class Extensions
    {
        private static RodriguezModel db = new RodriguezModel();

        public static bool containsProduct(this ListaCompra lista, int productoId)
        {
            var productos = lista.Productos;
            var producto = db.Productos.FindAsync(productoId);

            if(producto == null || productos == null || productos.Count() == 0)
            {
                return false;
            }
            
            foreach(var p in productos)
            {
                if(p.ProductoId == productoId)
                {
                    return true;
                }
            }

            return false;
        }
    }
}