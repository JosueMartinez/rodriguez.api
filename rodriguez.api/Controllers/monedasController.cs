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
    public class monedasController : ApiController
    {
        private RodriguezModel db = new RodriguezModel();

        // GET: api/monedas
        public IQueryable<moneda> Getmonedas()
        {
            return db.monedas;
        }

        // GET: api/monedas/5
        [ResponseType(typeof(moneda))]
        public async Task<IHttpActionResult> Getmoneda(int id)
        {
            moneda moneda = await db.monedas.FindAsync(id);
            if (moneda == null)
            {
                return NotFound();
            }

            return Ok(moneda);
        }

        // PUT: api/monedas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putmoneda(int id, moneda moneda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != moneda.id)
            {
                return BadRequest();
            }

            db.Entry(moneda).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!monedaExists(id))
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

        // POST: api/monedas
        [ResponseType(typeof(moneda))]
        public async Task<IHttpActionResult> Postmoneda(moneda moneda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.monedas.Add(moneda);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = moneda.id }, moneda);
        }

        // DELETE: api/monedas/5
        [ResponseType(typeof(moneda))]
        public async Task<IHttpActionResult> Deletemoneda(int id)
        {
            moneda moneda = await db.monedas.FindAsync(id);
            if (moneda == null)
            {
                return NotFound();
            }

            db.monedas.Remove(moneda);
            await db.SaveChangesAsync();

            return Ok(moneda);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool monedaExists(int id)
        {
            return db.monedas.Count(e => e.id == id) > 0;
        }
    }
}