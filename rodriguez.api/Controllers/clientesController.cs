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
    public class ClientesController : ApiController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public ClientesController()
        {
        }

        // GET: api/Clientes
        public IEnumerable GetClientes()
        {
            return unitOfWork.Clientes.Get();
        }

        // GET: api/Clientes/5
        [ResponseType(typeof(Cliente))]
        public IHttpActionResult GetCliente(int id)
        {
            Cliente Cliente = unitOfWork.Clientes.Get(id);
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
            Cliente Cliente = unitOfWork.Clientes.Get().Where(x => x.Usuario.Equals(Usuario)).FirstOrDefault();
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

            unitOfWork.Clientes.Update(Cliente);

            try
            {
                unitOfWork.Commit();
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

            unitOfWork.Clientes.Insert(Cliente);
            unitOfWork.Commit();

            return CreatedAtRoute("DefaultApi", new { id = Cliente.Id }, Cliente);
        }

        // DELETE: api/Clientes/5
        [ResponseType(typeof(Cliente))]
        public IHttpActionResult DeleteCliente(int id)
        {
            Cliente Cliente = unitOfWork.Clientes.Get(id);
            if (Cliente == null)
            {
                return NotFound();
            }

            unitOfWork.Clientes.Delete(id);
            unitOfWork.Commit();

            return Ok(Cliente);
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        private bool ClienteExists(int id)
        {
            return unitOfWork.Clientes.Get(id) != null;
        }
    }
}