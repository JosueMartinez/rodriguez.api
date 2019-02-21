using Rodriguez.Data.Models;
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

namespace rodriguez.api.Controllers
{
    [Authorize]
    public class UsuariosController : ApiController
    {
        private RodriguezModel db = new RodriguezModel();

        // GET: api/Usuarios
        public IQueryable<Usuario> GetUsuarios()
        {
            return db.Usuarios.Include(x => x.rol).Where(x => x.Activo);
        }

        // GET: api/Usuarios/5
        [ResponseType(typeof(Usuario))]
        public async Task<IHttpActionResult> GetUsuario(int id)
        {
            var Usuarios =  db.Usuarios.Include(x => x.rol).Where(x => x.Id == id);
            var Usuario = Usuarios.Count() > 0 ? await Usuarios.FirstAsync() : null;

            if (Usuario == null)
            {
                return NotFound();
            }
            if (Usuario.Activo)
            {
                return Ok(Usuario);
            }

            return NotFound();
        }

        // GET: api/Clientes/5
        [ResponseType(typeof(Bono))]
        [Route("api/UsuarioU/{Usuario}")]
        [HttpGet] //
        public async Task<IHttpActionResult> GetClienteNombre(string Usuario)
        {
            Usuario u = await db.Usuarios.Include(x => x.rol).Where(x => x.NombreUsuario.ToLower().Equals(Usuario.ToLower()) && x.Activo).FirstOrDefaultAsync();
            if (u == null)
            {
                return NotFound();
            }

            return Ok(u);
        }

        // PUT: api/Usuarios/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUsuario(int id, Usuario Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Usuario.Id)
            {
                return BadRequest();
            }

            db.Entry(Usuario).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuarios
        [ResponseType(typeof(Usuario))]
        public async Task<IHttpActionResult> PostUsuario(Usuario Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Usuarios.Add(Usuario);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = Usuario.Id }, Usuario);
        }

        // DELETE: api/Usuarios/5
        [ResponseType(typeof(Usuario))]
        public async Task<IHttpActionResult> DeleteUsuario(int id)
        {
            Usuario Usuario = await db.Usuarios.FindAsync(id);
            if (Usuario == null)
            {
                return NotFound();
            }

            db.Usuarios.Remove(Usuario);
            await db.SaveChangesAsync();

            return Ok(Usuario);
        }

        // GET: api/Roles
        [Route("api/Rol")]
        [HttpGet]
        public IQueryable<Rol> GetRoles()
        {
            return db.Roles;//.Include(c => c.Productos).OrderBy(c => c.Descripcion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsuarioExists(int id)
        {
            return db.Usuarios.Count(e => e.Id == id) > 0;
        }
    }
}