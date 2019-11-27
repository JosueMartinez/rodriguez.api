﻿using Rodriguez.Data.Models;
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

        private readonly UnitOfWork unitOfWork = new UnitOfWork();

        public UsuariosController()
        {
        }

        // GET: api/Usuarios
        public IEnumerable GetUsuarios()
        {
            return unitOfWork.Usuarios.Get().Where(x => x.Activo);
        }

        // GET: api/Usuarios/5
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult GetUsuario(int id)
        {
            var Usuario = unitOfWork.Usuarios.Get(id);

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
            Usuario u = unitOfWork.UsuariosCustom.GetClienteNombre(Usuario);
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

            unitOfWork.Usuarios.Update(Usuario);

            try
            {
                unitOfWork.Commit();
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
            Usuario usuario = unitOfWork.Usuarios.Get(usuarioId);
            if (usuario == null)
                return NotFound();

            Rol rol = unitOfWork.Roles.Get(RolId);
            if (rol == null)
                return BadRequest();

            usuario.RolId = rol.Id;
            usuario.ConfirmarContrasena = usuario.Contrasena;
            unitOfWork.Usuarios.Update(usuario);

            try
            {
                unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        // POST: api/Usuarios
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult PostUsuario(Usuario Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.Usuarios.Insert(Usuario);
            unitOfWork.Commit();

            return CreatedAtRoute("DefaultApi", new { id = Usuario.Id }, Usuario);
        }

        // DELETE: api/Usuarios/5
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult DeleteUsuario(int id)
        {
            Usuario Usuario = unitOfWork.Usuarios.Get(id);
            if (Usuario == null)
            {
                return NotFound();
            }

            unitOfWork.UsuariosCustom.DisableUsuario(Usuario);

            try
            {
                unitOfWork.Commit();
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
            return unitOfWork.Roles.Get();
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        private bool UsuarioExists(int id)
        {
            return unitOfWork.Usuarios.Get(id) != null;
        }
    }
}