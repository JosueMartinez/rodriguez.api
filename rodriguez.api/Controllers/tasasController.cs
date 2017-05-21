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
    public class tasasController : ApiController
    {
        private RodriguezModel db = new RodriguezModel();

        // GET: api/tasa
        public IQueryable<tasamoneda> Gettasasmonedas()
        {
            return db.tasasmonedas;
        }

        // GET: api/tasa/5
        [ResponseType(typeof(tasamoneda))]
        [Route("api/monedas/{monedaId:int}/tasa")]
        [HttpGet]
        public async Task<IHttpActionResult> Gettasamoneda(int monedaId)
        {
            using(RodriguezModel db = new RodriguezModel())
            {
                var moneda = await db.monedas.FindAsync(monedaId);
                if (moneda == null)
                {
                    return NotFound();
                }
                                
                var tasasFecha = db.tasasmonedas.Where(x => x.moneda.id == monedaId).OrderByDescending(x => x.fecha);
                var tasa = tasasFecha.Count() > 0 ? await tasasFecha.Include(m => m.moneda).FirstAsync() : null;
                
                if (tasa == null)
                {
                    return NotFound();
                }

                return Ok(tasa);
                
            }
        }

        //changing
        [ResponseType(typeof(tasamoneda))]
        [Route("api/monedas/{simbolo}/tasa")]
        [HttpGet]
        public async Task<IHttpActionResult> Gettasamoneda(string simbolo)
        {
            try
            {
                if (db.monedas.Count(x => x.simbolo.Equals(simbolo)) == 0)
                {
                    return NotFound();
                }

                var tasasFecha = db.tasasmonedas.Where(x => x.moneda.simbolo.Equals(simbolo)).OrderByDescending(x => x.fecha);
                var tasa = tasasFecha.Count() > 0 ? await tasasFecha.Include(m => m.moneda).FirstAsync(): null;
                if (tasa == null)
                {
                    return NotFound();
                }

                return Ok(tasa);
            }
            catch(Exception e)
            {
                return InternalServerError(e);
            }
            
        }

        // PUT: api/tasa/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Puttasamoneda(int id, tasamoneda tasamoneda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tasamoneda.id)
            {
                return BadRequest();
            }

            db.Entry(tasamoneda).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tasamonedaExists(id))
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

        // POST: api/tasa
        [ResponseType(typeof(tasamoneda))]
        public async Task<IHttpActionResult> Posttasamoneda(tasamoneda tasa)
        {
            //agregar fecha y activar
            try
            {
                tasa.fecha = DateTime.Now;
                tasa.activa = true;
                var monedas = db.monedas.Where(x => x.id == tasa.monedaId);

                if(tasa.valor <= 0 && monedas.Count() == 0)
                {
                    return BadRequest();
                }

                tasa.moneda = monedas.First();
                disableTasas(tasa.moneda.id);    //desactivando todas demas tasas
                db.tasasmonedas.Add(tasa);
                await db.SaveChangesAsync();

                return Ok(tasa);
            }catch(Exception e)
            {
                return InternalServerError();
            }
            
        }

        // DELETE: api/tasa/5
        [ResponseType(typeof(tasamoneda))]
        public async Task<IHttpActionResult> Deletetasamoneda(int id)
        {
            tasamoneda tasamoneda = await db.tasasmonedas.FindAsync(id);
            if (tasamoneda == null)
            {
                return NotFound();
            }

            db.tasasmonedas.Remove(tasamoneda);
            await db.SaveChangesAsync();

            return Ok(tasamoneda);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tasamonedaExists(int id)
        {
            return db.tasasmonedas.Count(e => e.id == id) > 0;
        }

        private void disableTasas(int monedaId)
        {
            var tasas = db.tasasmonedas.Where(x => x.moneda.id ==(monedaId));
            tasas.ForEachAsync((tasamoneda t) => {
                t.activa = false;
                db.Entry(t).State = EntityState.Modified;
            });

            db.SaveChanges();
        }
    }
}