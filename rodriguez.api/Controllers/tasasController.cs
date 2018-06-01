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
using System.Web.Http.Cors;

namespace rodriguez.api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Authorize]
    public class tasasController : ApiController
    {
        private RodriguezModel db = new RodriguezModel();

        // GET: api/tasa
        public IQueryable<Tasa> Gettasasmonedas()
        {
            return db.Tasas.Include("moneda").Where(x => x.Activa);
        }

        [ResponseType(typeof(Tasa))]
        [Route("api/monedas/{monedaId:int}/historial")]
        [HttpGet]
        public IQueryable<Tasa> GetHistorial(int monedaId)
        {
            return db.Tasas.Where(x => x.MonedaId == monedaId).OrderByDescending(x => x.Fecha).Take(10);
        }
        
        [ResponseType(typeof(Tasa))]
        [Route("api/monedas/{monedaId:int}/tasa")]
        [HttpGet]
        public async Task<IHttpActionResult> Gettasamoneda(int monedaId)
        {
            using(RodriguezModel db = new RodriguezModel())
            {
                var moneda = await db.Monedas.FindAsync(monedaId);
                if (moneda == null)
                {
                    return NotFound();
                }
                                
                var tasasFecha = db.Tasas.Where(x => x.Moneda.Id == monedaId).OrderByDescending(x => x.Fecha);
                var tasa = tasasFecha.Count() > 0 ? await tasasFecha.Include(m => m.Moneda).FirstAsync() : null;
                
                if (tasa == null)
                {
                    return NotFound();
                }

                return Ok(tasa);
                
            }
        }

        //changing
        [ResponseType(typeof(Tasa))]
        [Route("api/monedas/{simbolo}/tasa")]
        [HttpGet]
        public async Task<IHttpActionResult> Gettasamoneda(string simbolo)
        {
            try
            {
                if (db.Monedas.Count(x => x.Simbolo.Equals(simbolo)) == 0)
                {
                    return NotFound();
                }

                var tasasFecha = db.Tasas.Where(x => x.Moneda.Simbolo.Equals(simbolo)).OrderByDescending(x => x.Fecha);
                var tasa = tasasFecha.Count() > 0 ? await tasasFecha.Include(m => m.Moneda).FirstAsync(): null;
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
        public async Task<IHttpActionResult> Puttasamoneda(int id, Tasa tasamoneda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tasamoneda.Id)
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
        [ResponseType(typeof(Tasa))]
        public async Task<IHttpActionResult> Posttasamoneda(Tasa tasa)
        {
            //agregar fecha y activar
            try
            {
                tasa.Fecha = DateTime.Now;
                tasa.Activa = true;
                var monedas = db.Monedas.Where(x => x.Id == tasa.Moneda.Id);

                if(tasa.Valor <= 0 && monedas.Count() == 0)
                {
                    return BadRequest();
                }

                tasa.Moneda = monedas.First();
                disableTasas(tasa.Moneda.Id);    //desactivando todas demas tasas
                db.Tasas.Add(tasa);
                await db.SaveChangesAsync();

                return Ok(tasa);
            }catch(Exception e)
            {
                return InternalServerError();
            }
            
        }

        // DELETE: api/tasa/5
        [ResponseType(typeof(Tasa))]
        public async Task<IHttpActionResult> Deletetasamoneda(int id)
        {
            Tasa tasamoneda = await db.Tasas.FindAsync(id);
            if (tasamoneda == null)
            {
                return NotFound();
            }

            db.Tasas.Remove(tasamoneda);
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
            return db.Tasas.Count(e => e.Id == id) > 0;
        }

        private void disableTasas(int monedaId)
        {
            var tasas = db.Tasas.Where(x => x.Moneda.Id ==(monedaId));
            tasas.ForEachAsync((Tasa t) => {
                t.Activa = false;
                db.Entry(t).State = EntityState.Modified;
            });

            db.SaveChanges();
        }
    }
}