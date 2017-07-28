using Microsoft.AspNet.Identity;
using rodriguez.api.Clases;
using rodriguez.api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace rodriguez.api.Controllers
{
    [RoutePrefix("api/Account")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountController : ApiController
    {
        private AuthRepository _repo = null;
        private RodriguezModel db = new RodriguezModel();

        public AccountController()
        {
            _repo = new AuthRepository();
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(usuario userModel)
        {
            //RodriguezModel db = new RodriguezModel();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

                try
                {
                    rol r = db.rols.Where(rx => rx.descripcion.Equals("Empleado")).FirstOrDefault();    // TODO get rol correspondiente
                    userModel.rol = r;
                    userModel.rolId = r.id;
                    userModel.activo = true;
                    db.usuarios.Add(userModel);
                    await db.SaveChangesAsync();
                    IdentityResult result = await _repo.RegisterUser(userModel);

                    IHttpActionResult errorResult = GetErrorResult(result);

                    if (errorResult != null)
                    {
                        return errorResult;
                    }

                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest(e.ToString());
                }
            
            
        }

        // POST api/Account/RegisterClient
        [AllowAnonymous]
        [Route("RegisterClient")]
        public async Task<IHttpActionResult> RegisterClient(cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                db.clientes.Add(cliente);
                await db.SaveChangesAsync();
                IdentityResult result = await _repo.RegisterClient(cliente);

                IHttpActionResult errorResult = GetErrorResult(result);

                if (errorResult != null)
                {
                    return errorResult;
                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }




        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
