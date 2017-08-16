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

namespace rodriguez.api.Controllers
{
    public class usuariosController : ApiController
    {
        private RodriguezModel db = new RodriguezModel();

        // GET: api/usuarios
        public IQueryable<usuario> Getusuarios()
        {
            return db.usuarios;
        }

        // GET: api/usuarios/5
        [ResponseType(typeof(usuario))]
        public async Task<IHttpActionResult> Getusuario(int id)
        {
            usuario usuario = await db.usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        // GET: api/clientes/5
        [ResponseType(typeof(bono))]
        [Route("api/usuarioU/{usuario}")]
        [HttpGet] //
        public async Task<IHttpActionResult> GetclienteNombre(string usuario)
        {
            usuario u = await db.usuarios.Include(x => x.rol).Where(x => x.nombreUsuario.ToLower().Equals(usuario.ToLower())).FirstOrDefaultAsync();
            if (u == null)
            {
                return NotFound();
            }

            return Ok(u);
        }

        // PUT: api/usuarios/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putusuario(int id, usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuario.id)
            {
                return BadRequest();
            }

            db.Entry(usuario).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!usuarioExists(id))
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

        // POST: api/usuarios
        [ResponseType(typeof(usuario))]
        public async Task<IHttpActionResult> Postusuario(usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.usuarios.Add(usuario);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = usuario.id }, usuario);
        }

        // DELETE: api/usuarios/5
        [ResponseType(typeof(usuario))]
        public async Task<IHttpActionResult> Deleteusuario(int id)
        {
            usuario usuario = await db.usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            db.usuarios.Remove(usuario);
            await db.SaveChangesAsync();

            return Ok(usuario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool usuarioExists(int id)
        {
            return db.usuarios.Count(e => e.id == id) > 0;
        }
    }
}