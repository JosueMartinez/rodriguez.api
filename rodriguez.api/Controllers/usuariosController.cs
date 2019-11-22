using Rodriguez.Data.Models;
using Rodriguez.Repo;
using System;
using System.Collections;
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
        //private RodriguezModel db = new RodriguezModel();
        private readonly Repository<Usuario> repo = null;
        private readonly UsuarioRepository userRepo = null;
        private readonly Repository<Rol> rolRepo = null;

        public UsuariosController()
        {
            repo = new Repository<Usuario>();
            userRepo = new UsuarioRepository();
            rolRepo = new Repository<Rol>();
        }

        // GET: api/Usuarios
        public IEnumerable GetUsuarios()
        {
            return repo.Get().Where(x => x.Activo);
        }

        // GET: api/Usuarios/5
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult GetUsuario(int id)
        {
            var Usuario = repo.Get(id);

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
        public IHttpActionResult GetClienteNombre(string Usuario)
        {
            Usuario u = userRepo.GetClienteNombre(Usuario);
            if (u == null)
            {
                return NotFound();
            }

            return Ok(u);
        }

        // PUT: api/Usuarios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUsuario(int id, Usuario Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Usuario.Id)
            {
                return BadRequest();
            }

            repo.Update(Usuario);

            try
            {
                repo.Save();
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

        [ResponseType(typeof(void))]
        [Route("api/Usuario/{usuarioId}/Rol/{RolId}")]
        [HttpPut]
        public async Task<IHttpActionResult> CambiarRol(int usuarioId, int RolId)
        {
            Usuario usuario = repo.Get(usuarioId);
            if (usuario == null)
                return NotFound();

            Rol rol = rolRepo.Get(RolId);
            if (rol == null)
                return BadRequest();

            usuario.RolId = rol.Id;
            usuario.ConfirmarContrasena = usuario.Contrasena;
            repo.Update(usuario);

            try
            {
                repo.Save();
            }
            catch (Exception e)
            {
                throw e;
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

            repo.Insert(Usuario);
            repo.Save();

            return CreatedAtRoute("DefaultApi", new { id = Usuario.Id }, Usuario);
        }

        // DELETE: api/Usuarios/5
        [ResponseType(typeof(Usuario))]
        public async Task<IHttpActionResult> DeleteUsuario(int id)
        {
            Usuario Usuario = repo.Get(id);
            if (Usuario == null)
            {
                return NotFound();
            }

            userRepo.DisableUsuario(Usuario);

            try
            {
                repo.Save();
            }
            catch (Exception e)
            {
                throw e;
            }

            return Ok();
        }

        // GET: api/Roles
        [Route("api/Rol")]
        [HttpGet]
        public IEnumerable GetRoles()
        {
            return rolRepo.Get();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsuarioExists(int id)
        {
            return repo.Get(id) != null;
        }
    }
}