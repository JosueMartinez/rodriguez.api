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
    public class categoriasController : ApiController
    {
        private RodriguezModel db = new RodriguezModel();

        // GET: api/categorias
        public IQueryable<Categoria> Getcategorias()
        {
            return db.Categorias.Include(c => c.Productos).OrderBy(c => c.Descripcion);
        }

        // GET: api/categorias/5
        [ResponseType(typeof(Categoria))]
        public async Task<IHttpActionResult> Getcategoria(int id)
        {
            Categoria categoria = await db.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            return Ok(categoria);
        }

        // PUT: api/categorias/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putcategoria(int id, Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categoria.Id)
            {
                return BadRequest();
            }

            db.Entry(categoria).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!categoriaExists(id))
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

        // POST: api/categorias
        [ResponseType(typeof(Categoria))]
        public async Task<IHttpActionResult> Postcategoria(Categoria categoria)
        {
            if (categoria == null)  //el request no contiene categoria
            {
                return BadRequest(ModelState);
            }

            //si la descripcion de la categoria esta en blanco
            if (String.IsNullOrEmpty(categoria.Descripcion) || String.IsNullOrWhiteSpace(categoria.Descripcion))
            {
                return BadRequest("La categoría debe tener una descripción.");
            }

            try
            {
                categoria.Descripcion = Utilidades.capitalize(categoria.Descripcion);
                //verificar si no existe una categoria con el mismo nombre
                if (categoriaExists(categoria.Descripcion))
                {
                    db.Categorias.Add(categoria);
                    await db.SaveChangesAsync();
                    return CreatedAtRoute("DefaultApi", new { id = categoria.Id }, categoria);
                }
                return BadRequest("Esta categoría ya existe");
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // DELETE: api/categorias/5
        [ResponseType(typeof(Categoria))]
        public async Task<IHttpActionResult> Deletecategoria(int id)
        {
            Categoria categoria = await db.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            db.Categorias.Remove(categoria);
            await db.SaveChangesAsync();

            return Ok(categoria);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public bool categoriaExists(int id)
        {
            return db.Categorias.Count(e => e.Id == id) > 0;
        }

        private bool categoriaExists(String descripcion)
        {
            return db.Categorias.Where(x => x.Descripcion.Equals(descripcion)).Count() == 0;
        }
    }
}