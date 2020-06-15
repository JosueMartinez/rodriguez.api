using Microsoft.AspNet.Identity;
using rodriguez.api.Clases;
using Rodriguez.Data.Models;
using Rodriguez.Repo;
using Rodriguez.Repo.Interfaces;
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
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountController : ApiController
    {
        private readonly AuthRepository _repo = null;
        private readonly IUnitOfWork _unitOfWork;


        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repo = new AuthRepository();
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(Usuario userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                userModel.Activo = true;
                _unitOfWork.Usuarios.Insert(userModel);
                _unitOfWork.Commit();

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
        public async Task<IHttpActionResult> RegisterClient(Cliente Cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _unitOfWork.Clientes.Insert(Cliente);
                _unitOfWork.Commit();
                IdentityResult result = await _repo.RegisterClient(Cliente);

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
                _unitOfWork.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
