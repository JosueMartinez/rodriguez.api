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
using rodriguez.api.Models;
using rodriguez.api.Clases;

namespace rodriguez.api.Controllers
{
    [Authorize]
    public class productosController : ApiController
    {
        private RodriguezModel db = new RodriguezModel();

        // GET: api/productos
        public IQueryable<Producto> Getproductos()
        {   
            return db.Productos.Include("categoria").Include("medida");
        }

        // GET: api/productos/5
        [ResponseType(typeof(Producto))]
        public async Task<IHttpActionResult> Getproducto(int id)
        {
            Producto producto = await db.Productos.Where(x => x.Id == id).Include("categoria").Include("medida").FirstOrDefaultAsync();//.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        // PUT: api/productos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putproducto(int id, Producto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != producto.Id)
            {
                return BadRequest();
            }

            db.Entry(producto).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!productoExists(id))
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

        // POST: api/productosLista
        [ResponseType(typeof(Producto))]
        public async Task<IHttpActionResult> Postproducto(Producto producto)
        {
            if (producto == null)  //el request no contiene producto
            {
                return BadRequest(ModelState);
            }

            //si el nombre del producto esta en blanco
            if (String.IsNullOrEmpty(producto.Nombre) || String.IsNullOrWhiteSpace(producto.Nombre))
            {
                return BadRequest("El producto debe tener un nombre.");
            }

            producto.Nombre = Utilidades.capitalize(producto.Nombre);
            if (productoExists(producto.Nombre))
            {
                #region obtener categoria
                categoriasController c = new categoriasController();
                if (!c.categoriaExists(producto.CategoriaId))
                {
                    return BadRequest("Debe pertenecer a una categoría válida");
                }
                producto.Categoria = db.Categorias.Find(producto.CategoriaId);

                #endregion

                #region obtener medida
                medidasController m = new medidasController();
                if (!m.medidaExists(producto.MedidaId))
                {
                    return BadRequest("Debe pertenecer a una categoría válida");
                }
                producto.Medida = db.Medidas.Find(producto.CategoriaId);
                #endregion

                try
                {
                    db.Productos.Add(producto);
                    await db.SaveChangesAsync();
                    return CreatedAtRoute("DefaultApi", new { id = producto.Id }, producto);
                }
                catch (Exception e)
                {
                    return InternalServerError(e);
                }
            }
            return BadRequest("Este producto ya existe");
            
        }

        // DELETE: api/productosLista/5
        [ResponseType(typeof(Producto))]
        public async Task<IHttpActionResult> Deleteproducto(int id)
        {
            Producto producto = await db.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            db.Productos.Remove(producto);
            await db.SaveChangesAsync();

            return Ok(producto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool productoExists(int id)
        {
            return db.Productos.Count(e => e.Id == id) > 0;
        }

        private bool productoExists(String nombre)
        {
            return db.Productos.Where(x => x.Nombre.Equals(nombre)).Count() == 0;
        }
    }
}