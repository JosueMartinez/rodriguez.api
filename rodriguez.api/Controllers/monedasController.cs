using Rodriguez.Data.Models;
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

namespace rodriguez.api.Controllers
{
    public class MonedasController : ApiController
    {
        private RodriguezModel db = new RodriguezModel();

        // GET: api/Monedas
        public IQueryable<Moneda> GetMonedas()
        {
            var query = from m in db.Monedas
                        select new
                        {
                             Moneda = m,
                             Tasas = m.Tasas.Where(t => t.Activa == true)
                        };

            var Monedas = query.ToArray().Select(m => m.Moneda);
            return Monedas.AsQueryable();
        }

        // GET: api/Monedas/5
        [ResponseType(typeof(Moneda))]
        public async Task<IHttpActionResult> GetMoneda(int id)
        {
            Moneda Moneda = await db.Monedas.FindAsync(id);
            if (Moneda == null)
            {
                return NotFound();
            }

            return Ok(Moneda);
        }

        // PUT: api/Monedas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMoneda(int id, Moneda Moneda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Moneda.Id)
            {
                return BadRequest();
            }

            db.Entry(Moneda).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MonedaExists(id))
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

        // POST: api/Monedas
        [ResponseType(typeof(Moneda))]
        public async Task<IHttpActionResult> PostMoneda(Moneda Moneda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Monedas.Add(Moneda);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = Moneda.Id }, Moneda);
        }

        // DELETE: api/Monedas/5
        [ResponseType(typeof(Moneda))]
        public async Task<IHttpActionResult> DeleteMoneda(int id)
        {
            Moneda Moneda = await db.Monedas.FindAsync(id);
            if (Moneda == null)
            {
                return NotFound();
            }

            db.Monedas.Remove(Moneda);
            await db.SaveChangesAsync();

            return Ok(Moneda);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MonedaExists(int id)
        {
            return db.Monedas.Count(e => e.Id == id) > 0;
        }
    }
}