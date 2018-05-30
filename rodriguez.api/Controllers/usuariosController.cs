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
    [Authorize]
    public class usuariosController : ApiController
    {
        private RodriguezModel db = new RodriguezModel();

        // GET: api/usuarios
        public IQueryable<Usuario> Getusuarios()
        {
            return db.Usuarios.Include(x => x.Rol).Where(x => x.Activo);
        }

        // GET: api/usuarios/5
        [ResponseType(typeof(Usuario))]
        public async Task<IHttpActionResult> Getusuario(int id)
        {
            var usuarios =  db.Usuarios.Include(x => x.Rol).Where(x => x.Id == id);
            var usuario = usuarios.Count() > 0 ? await usuarios.FirstAsync() : null;

            if (usuario == null)
            {
                return NotFound();
            }
            if (usuario.Activo)
            {
                return Ok(usuario);
            }

            return NotFound();
        }

        // GET: api/clientes/5
        [ResponseType(typeof(Bono))]
        [Route("api/usuarioU/{usuario}")]
        [HttpGet] //
        public async Task<IHttpActionResult> GetclienteNombre(string usuario)
        {
            Usuario u = await db.Usuarios.Include(x => x.Rol).Where(x => x.NombreUsuario.ToLower().Equals(usuario.ToLower()) && x.Activo).FirstOrDefaultAsync();
            if (u == null)
            {
                return NotFound();
            }

            return Ok(u);
        }

        // PUT: api/usuarios/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putusuario(int id, Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuario.Id)
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
        [ResponseType(typeof(Usuario))]
        public async Task<IHttpActionResult> Postusuario(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Usuarios.Add(usuario);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = usuario.Id }, usuario);
        }

        // DELETE: api/usuarios/5
        [ResponseType(typeof(Usuario))]
        public async Task<IHttpActionResult> Deleteusuario(int id)
        {
            Usuario usuario = await db.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            db.Usuarios.Remove(usuario);
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
            return db.Usuarios.Count(e => e.Id == id) > 0;
        }
    }
}