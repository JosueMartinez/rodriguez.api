using Rodriguez.Data.Models;
using Rodriguez.Repo;
using Rodriguez.Repo.Interfaces;
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
    public class ClientesController : ApiController
    {
        private IUnitOfWork _unitOfWork;

        public ClientesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Clientes
        public IEnumerable GetClientes()
        {
            return _unitOfWork.Clientes.Get();
        }

        // GET: api/Clientes/5
        [ResponseType(typeof(Cliente))]
        public IHttpActionResult GetCliente(int id)
        {
            Cliente Cliente = _unitOfWork.Clientes.Get(id);
            if (Cliente == null)
            {
                return NotFound();
            }

            return Ok(Cliente);
        }

        // GET: api/Clientes/5
        [ResponseType(typeof(Bono))]
        [Route("api/ClienteU/{Usuario}")]
        [HttpGet]
        public IHttpActionResult GetClienteNombre(string Usuario)
        {
            Cliente Cliente = _unitOfWork.Clientes.Get().Where(x => x.Usuario.Equals(Usuario)).FirstOrDefault();
            if (Cliente == null)
            {
                return NotFound();
            }

            return Ok(Cliente);
        }

        // PUT: api/Clientes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCliente(int id, Cliente Cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Cliente.ClienteId)
            {
                return BadRequest();
            }

            _unitOfWork.Clientes.Update(Cliente);

            try
            {
                _unitOfWork.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        // POST: api/Clientes
        [ResponseType(typeof(Cliente))]
        public async Task<IHttpActionResult> PostCliente(Cliente Cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.Clientes.Insert(Cliente);
            _unitOfWork.Commit();

            return CreatedAtRoute("DefaultApi", new { id = Cliente.Id }, Cliente);
        }

        // DELETE: api/Clientes/5
        [ResponseType(typeof(Cliente))]
        public IHttpActionResult DeleteCliente(int id)
        {
            Cliente Cliente = _unitOfWork.Clientes.Get(id);
            if (Cliente == null)
            {
                return NotFound();
            }

            _unitOfWork.Clientes.Delete(id);
            _unitOfWork.Commit();

            return Ok(Cliente);
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        private bool ClienteExists(int id)
        {
            return _unitOfWork.Clientes.Get(id) != null;
        }
    }
}