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
    public class productosController : ApiController
    {
        private RodriguezModel db = new RodriguezModel();

        // GET: api/productos
        public IQueryable<producto> Getproductos()
        {   
            return db.productos.Include("categoria").Include("medida");
        }

        // GET: api/productos/5
        [ResponseType(typeof(producto))]
        public async Task<IHttpActionResult> Getproducto(int id)
        {
            producto producto = await db.productos.Where(x => x.id == id).Include("categoria").Include("medida").FirstOrDefaultAsync();//.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        // PUT: api/productos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putproducto(int id, producto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != producto.id)
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
        [ResponseType(typeof(producto))]
        public async Task<IHttpActionResult> Postproducto(producto producto)
        {
            if (producto == null)  //el request no contiene producto
            {
                return BadRequest(ModelState);
            }

            //si el nombre del producto esta en blanco
            if (String.IsNullOrEmpty(producto.nombre) || String.IsNullOrWhiteSpace(producto.nombre))
            {
                return BadRequest("El producto debe tener un nombre.");
            }

            producto.nombre = Utilidades.capitalize(producto.nombre);
            if (productoExists(producto.nombre))
            {
                #region obtener categoria
                categoriasController c = new categoriasController();
                if (!c.categoriaExists(producto.categoriaId))
                {
                    return BadRequest("Debe pertenecer a una categoría válida");
                }
                producto.categoria = db.categorias.Find(producto.categoriaId);

                #endregion

                #region obtener medida
                medidasController m = new medidasController();
                if (!m.medidaExists(producto.medidaId))
                {
                    return BadRequest("Debe pertenecer a una categoría válida");
                }
                producto.medida = db.medidas.Find(producto.categoriaId);
                #endregion

                try
                {
                    db.productos.Add(producto);
                    await db.SaveChangesAsync();
                    return CreatedAtRoute("DefaultApi", new { id = producto.id }, producto);
                }
                catch (Exception e)
                {
                    return InternalServerError(e);
                }
            }
            return BadRequest("Este producto ya existe");
            
        }

        // DELETE: api/productosLista/5
        [ResponseType(typeof(producto))]
        public async Task<IHttpActionResult> Deleteproducto(int id)
        {
            producto producto = await db.productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            db.productos.Remove(producto);
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
            return db.productos.Count(e => e.id == id) > 0;
        }

        private bool productoExists(String nombre)
        {
            return db.productos.Where(x => x.nombre.Equals(nombre)).Count() == 0;
        }
    }
}