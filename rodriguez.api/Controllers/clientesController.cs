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
        //private RodriguezModel db = new RodriguezModel();
        private readonly Repository<Cliente> repo = null;

        public ClientesController()
        {
            repo = new Repository<Cliente>();
        }

        // GET: api/Clientes
        public IEnumerable GetClientes()
        {
            return repo.Get();
        }

        // GET: api/Clientes/5
        [ResponseType(typeof(Cliente))]
        public async Task<IHttpActionResult> GetCliente(int id)
        {
            Cliente Cliente = repo.Get(id);
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
            Cliente Cliente = repo.Get().Where(x => x.Usuario.Equals(Usuario)).FirstOrDefault();
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

            repo.Update(Cliente);

            try
            {
                repo.Save();
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

            repo.Insert(Cliente);
            repo.Save();

            return CreatedAtRoute("DefaultApi", new { id = Cliente.Id }, Cliente);
        }

        // DELETE: api/Clientes/5
        [ResponseType(typeof(Cliente))]
        public async Task<IHttpActionResult> DeleteCliente(int id)
        {
            Cliente Cliente = repo.Get(id);
            if (Cliente == null)
            {
                return NotFound();
            }

            repo.Delete(id);
            repo.Save();

            return Ok(Cliente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClienteExists(int id)
        {
            return repo.Get(id) != null;
        }
    }
}