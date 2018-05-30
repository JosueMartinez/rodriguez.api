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
        public IQueryable<Moneda> Getmonedas()
        {
            var query = from m in db.Monedas
                        select new
                        {
                             moneda = m,
                             tasas = m.Tasas.Where(t => t.Activa == true)
                        };

            var monedas = query.ToArray().Select(m => m.moneda);
            return monedas.AsQueryable();
        }

        // GET: api/monedas/5
        [ResponseType(typeof(Moneda))]
        public async Task<IHttpActionResult> Getmoneda(int id)
        {
            Moneda moneda = await db.Monedas.FindAsync(id);
            if (moneda == null)
            {
                return NotFound();
            }

            return Ok(moneda);
        }

        // PUT: api/monedas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putmoneda(int id, Moneda moneda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != moneda.Id)
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
        [ResponseType(typeof(Moneda))]
        public async Task<IHttpActionResult> Postmoneda(Moneda moneda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Monedas.Add(moneda);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = moneda.Id }, moneda);
        }

        // DELETE: api/monedas/5
        [ResponseType(typeof(Moneda))]
        public async Task<IHttpActionResult> Deletemoneda(int id)
        {
            Moneda moneda = await db.Monedas.FindAsync(id);
            if (moneda == null)
            {
                return NotFound();
            }

            db.Monedas.Remove(moneda);
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
            return db.Monedas.Count(e => e.Id == id) > 0;
        }
    }
}