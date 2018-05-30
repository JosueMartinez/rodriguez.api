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
    public class clientesController : ApiController
    {
        private RodriguezModel db = new RodriguezModel();

        // GET: api/clientes
        public IQueryable<Cliente> Getclientes()
        {
            return db.Clientes;
        }

        // GET: api/clientes/5
        [ResponseType(typeof(Cliente))]
        public async Task<IHttpActionResult> Getcliente(int id)
        {
            Cliente cliente = await db.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        // GET: api/clientes/5
        [ResponseType(typeof(Bono))]
        [Route("api/clienteU/{usuario}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetclienteNombre(string usuario)
        {
            Cliente cliente = await db.Clientes.Where(x => x.Usuario.Equals(usuario)).FirstOrDefaultAsync();
            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        // PUT: api/clientes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putcliente(int id, Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!id.Equals(cliente.Id))
            {
                return BadRequest();
            }

            db.Entry(cliente).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!clienteExists(id))
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

        // POST: api/clientes
        [ResponseType(typeof(Cliente))]
        public async Task<IHttpActionResult> Postcliente(Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Clientes.Add(cliente);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = cliente.Id }, cliente);
        }

        // DELETE: api/clientes/5
        [ResponseType(typeof(Cliente))]
        public async Task<IHttpActionResult> Deletecliente(int id)
        {
            Cliente cliente = await db.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            db.Clientes.Remove(cliente);
            await db.SaveChangesAsync();

            return Ok(cliente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool clienteExists(int id)
        {
            return db.Clientes.Count(e => e.Id.Equals(id)) > 0;
        }
    }
}