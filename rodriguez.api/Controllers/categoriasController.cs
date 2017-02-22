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
    public class categoriasController : ApiController
    {
        private RodriguezModel db = new RodriguezModel();

        //Obtener todas las categorias
        // GET: api/categorias
        public IQueryable<categoria> Getcategoria()
        {
            return db.categoria;
        }

        //Obtener una categoria en especifico
        // GET: api/categorias/{id}
        [ResponseType(typeof(categoria))]
        public async Task<IHttpActionResult> Getcategoria(int id)
        {
            categoria categoria = await db.categoria.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            return Ok(categoria);
        }

        //Actualizar categoria
        // PUT: api/categorias/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putcategoria(int id, categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categoria.id)
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

        //Insertar categoria
        // POST: api/categorias
        [ResponseType(typeof(categoria))]
        public async Task<IHttpActionResult> Postcategoria(categoria categoria)
        {
            if (categoria == null)  //el request no contiene categoria
            {
                return BadRequest(ModelState);
            }

            //si la descripcion de la categoria esta en blanco
            if(String.IsNullOrEmpty(categoria.descripcion) || String.IsNullOrWhiteSpace(categoria.descripcion))
            {
                return BadRequest("La categoría debe tener una descripción.");
            }

            try
            {
                categoria.descripcion = Utilidades.capitalize(categoria.descripcion);
                //verificar si no existe una categoria con el mismo nombre
                if(db.categoria.Where(x => x.descripcion.Equals(categoria.descripcion)).Count() == 0)
                {
                    db.categoria.Add(categoria);
                    await db.SaveChangesAsync();
                    return CreatedAtRoute("DefaultApi", new { id = categoria.id }, categoria);
                }
                return BadRequest("Esta categoría ya existe");
            }
            catch(Exception e)
            {
                return InternalServerError(e);
            }
        }

        // DELETE: api/categorias/5
        [ResponseType(typeof(categoria))]
        public async Task<IHttpActionResult> Deletecategoria(int id)
        {
            categoria categoria = await db.categoria.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            db.categoria.Remove(categoria);
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

        private bool categoriaExists(int id)
        {
            return db.categoria.Count(e => e.id == id) > 0;
        }
    }
}