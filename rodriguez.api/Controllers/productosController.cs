using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using rodriguez.api.Clases;
using Rodriguez.Data.Models;

namespace rodriguez.api.Controllers
{
    [Authorize]
    public class ProductosController : ApiController
    {
        private RodriguezModel db = new RodriguezModel();

        // GET: api/Productos
        public IQueryable<Producto> GetProductos()
        {   
            return db.Productos.Include("Categoria").Include("Medida");
        }

        // GET: api/Productos/5
        [ResponseType(typeof(Producto))]
        public async Task<IHttpActionResult> GetProducto(int id)
        {
            Producto Producto = await db.Productos.Where(x => x.Id == id).Include("Categoria").Include("Medida").FirstOrDefaultAsync();//.FindAsync(id);
            if (Producto == null)
            {
                return NotFound();
            }

            return Ok(Producto);
        }

        // PUT: api/Productos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProducto(int id, Producto Producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Producto.Id)
            {
                return BadRequest();
            }

            db.Entry(Producto).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ProductosLista
        [ResponseType(typeof(Producto))]
        public async Task<IHttpActionResult> PostProducto(Producto Producto)
        {
            if (Producto == null)  //el request no contiene Producto
            {
                return BadRequest(ModelState);
            }

            //si el Nombre del Producto esta en blanco
            if (String.IsNullOrEmpty(Producto.Nombre) || String.IsNullOrWhiteSpace(Producto.Nombre))
            {
                return BadRequest("El Producto debe tener un Nombre.");
            }

            Producto.Nombre = Utilidades.capitalize(Producto.Nombre);
            if (ProductoExists(Producto.Nombre))
            {
                #region obtener Categoria
                CategoriasController c = new CategoriasController();
                if (!c.CategoriaExists(Producto.CategoriaId))
                {
                    return BadRequest("Debe pertenecer a una categoría válida");
                }
                Producto.Categoria = db.Categorias.Find(Producto.CategoriaId);

                #endregion

                #region obtener Medida
                MedidasController m = new MedidasController();
                if (!m.MedidaExists(Producto.MedidaId))
                {
                    return BadRequest("Debe pertenecer a una categoría válida");
                }
                Producto.Medida = db.Medidas.Find(Producto.CategoriaId);
                #endregion

                try
                {
                    db.Productos.Add(Producto);
                    await db.SaveChangesAsync();
                    return CreatedAtRoute("DefaultApi", new { id = Producto.Id }, Producto);
                }
                catch (Exception e)
                {
                    return InternalServerError(e);
                }
            }
            return BadRequest("Este Producto ya existe");
            
        }

        // DELETE: api/ProductosLista/5
        [ResponseType(typeof(Producto))]
        public async Task<IHttpActionResult> DeleteProducto(int id)
        {
            Producto Producto = await db.Productos.FindAsync(id);
            if (Producto == null)
            {
                return NotFound();
            }

            db.Productos.Remove(Producto);
            await db.SaveChangesAsync();

            return Ok(Producto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductoExists(int id)
        {
            return db.Productos.Count(e => e.Id == id) > 0;
        }

        private bool ProductoExists(String Nombre)
        {
            return db.Productos.Where(x => x.Nombre.Equals(Nombre)).Count() == 0;
        }
    }
}