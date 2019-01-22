using Rodriguez.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rodriguez.api.Clases
{
    public static class Extensions
    {
        private static RodriguezModel db = new RodriguezModel();

        public static bool containsProduct(this ListaCompra lista, int ProductoId)
        {
            var Productos = lista.ProductosLista;
            var Producto = db.Productos.FindAsync(ProductoId);

            if(Producto == null || Productos == null || Productos.Count() == 0)
            {
                return false;
            }
            
            foreach(var p in Productos)
            {
                if(p.ProductoId == ProductoId)
                {
                    return true;
                }
            }

            return false;
        }
    }
}