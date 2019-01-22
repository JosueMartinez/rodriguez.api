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
    public class CategoriasController : ApiController
    {
        private RodriguezModel db = new RodriguezModel();

        // GET: api/Categorias
        public IQueryable<Categoria> GetCategorias()
        {
            return db.Categorias.Include(c => c.Productos).OrderBy(c => c.Descripcion);
        }

        // GET: api/Categorias/5
        [ResponseType(typeof(Categoria))]
        public async Task<IHttpActionResult> GetCategoria(int id)
        {
            Categoria Categoria = await db.Categorias.FindAsync(id);
            if (Categoria == null)
            {
                return NotFound();
            }

            return Ok(Categoria);
        }

        // PUT: api/Categorias/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCategoria(int id, Categoria Categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Categoria.Id)
            {
                return BadRequest();
            }

            db.Entry(Categoria).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(id))
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

        // POST: api/Categorias
        [ResponseType(typeof(Categoria))]
        public async Task<IHttpActionResult> PostCategoria(Categoria Categoria)
        {
            if (Categoria == null)  //el request no contiene Categoria
            {
                return BadRequest(ModelState);
            }

            //si la Descripcion de la Categoria esta en blanco
            if (String.IsNullOrEmpty(Categoria.Descripcion) || String.IsNullOrWhiteSpace(Categoria.Descripcion))
            {
                return BadRequest("La categoría debe tener una descripción.");
            }

            try
            {
                Categoria.Descripcion = Utilidades.capitalize(Categoria.Descripcion);
                //verificar si no existe una Categoria con el mismo Nombre
                if (CategoriaExists(Categoria.Descripcion))
                {
                    db.Categorias.Add(Categoria);
                    await db.SaveChangesAsync();
                    return CreatedAtRoute("DefaultApi", new { id = Categoria.Id }, Categoria);
                }
                return BadRequest("Esta categoría ya existe");
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // DELETE: api/Categorias/5
        [ResponseType(typeof(Categoria))]
        public async Task<IHttpActionResult> DeleteCategoria(int id)
        {
            Categoria Categoria = await db.Categorias.FindAsync(id);
            if (Categoria == null)
            {
                return NotFound();
            }

            db.Categorias.Remove(Categoria);
            await db.SaveChangesAsync();

            return Ok(Categoria);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public bool CategoriaExists(int id)
        {
            return db.Categorias.Count(e => e.Id == id) > 0;
        }

        private bool CategoriaExists(String Descripcion)
        {
            return db.Categorias.Where(x => x.Descripcion.Equals(Descripcion)).Count() == 0;
        }
    }
}