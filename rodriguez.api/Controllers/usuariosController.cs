using Microsoft.AspNet.Identity;
using rodriguez.api.Clases;
using Rodriguez.Data.Models;
using Rodriguez.Repo.Interfaces;
using System;
using System.Collections;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace rodriguez.api.Controllers
{
    [Authorize]
    public class UsuariosController : ApiController
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly AuthRepository _repo = null;

        public UsuariosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repo = new AuthRepository();
        }

        // GET: api/Usuarios
        public IEnumerable GetUsuarios()
        {
            return _unitOfWork.Usuarios.Get().Where(x => x.Activo);
        }

        // GET: api/Usuarios/5
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult GetUsuario(int id)
        {
            var Usuario = _unitOfWork.Usuarios.Get(id);

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
            Usuario u = _unitOfWork.UsuariosCustom.GetClienteNombre(Usuario);
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

            _unitOfWork.Usuarios.Update(Usuario);

            try
            {
                _unitOfWork.Commit();
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
        public IHttpActionResult CambiarRol(int usuarioId, int RolId)
        {
            Usuario usuario = _unitOfWork.Usuarios.Get(usuarioId);
            if (usuario == null)
                return NotFound();

            Rol rol = _unitOfWork.Roles.Get(RolId);
            if (rol == null)
                return BadRequest();

            usuario.RolId = rol.Id;
            usuario.ConfirmarContrasena = usuario.Contrasena;
            _unitOfWork.Usuarios.Update(usuario);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        // POST: api/Usuarios
        [ResponseType(typeof(Usuario))]
        public async System.Threading.Tasks.Task<IHttpActionResult> PostUsuarioAsync(Usuario Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.Usuarios.Insert(Usuario);
            _unitOfWork.Commit();

            await _repo.RegisterUser(Usuario);

            return CreatedAtRoute("DefaultApi", new { id = Usuario.Id }, Usuario);
        }

        // DELETE: api/Usuarios/5
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult DeleteUsuario(int id)
        {
            Usuario Usuario = _unitOfWork.Usuarios.Get(id);
            if (Usuario == null)
            {
                return NotFound();
            }

            _unitOfWork.UsuariosCustom.DisableUsuario(Usuario);

            try
            {
                _unitOfWork.Commit();
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
            return _unitOfWork.Roles.Get();
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        private bool UsuarioExists(int id)
        {
            return _unitOfWork.Usuarios.Get(id) != null;
        }
    }
}