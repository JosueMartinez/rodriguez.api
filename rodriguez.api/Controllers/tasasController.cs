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
using System.Web.Http.Cors;
using Rodriguez.Data.Models;

namespace rodriguez.api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Authorize]
    public class TasasController : ApiController
    {
        private RodriguezModel db = new RodriguezModel();

        // GET: api/Tasa
        public IQueryable<TasaMoneda> GetTasasMonedas()
        {
            return db.TasasMonedas.Include("Moneda").Where(x => x.Activa);
        }

        [ResponseType(typeof(TasaMoneda))]
        [Route("api/Monedas/{MonedaId:int}/historial")]
        [HttpGet]
        public IQueryable<TasaMoneda> GetHistorial(int MonedaId)
        {
            return db.TasasMonedas.Where(x => x.MonedaId == MonedaId).OrderByDescending(x => x.Fecha).Take(10);
        }
        
        [ResponseType(typeof(TasaMoneda))]
        [Route("api/Monedas/{MonedaId:int}/Tasa")]
        [HttpGet]
        public async Task<IHttpActionResult> GetTasaMoneda(int MonedaId)
        {
            using(RodriguezModel db = new RodriguezModel())
            {
                var Moneda = await db.Monedas.FindAsync(MonedaId);
                if (Moneda == null)
                {
                    return NotFound();
                }
                                
                var TasasFecha = db.TasasMonedas.Where(x => x.Moneda.Id == MonedaId).OrderByDescending(x => x.Fecha);
                var Tasa = TasasFecha.Count() > 0 ? await TasasFecha.Include(m => m.Moneda).FirstAsync() : null;
                
                if (Tasa == null)
                {
                    return NotFound();
                }

                return Ok(Tasa);
                
            }
        }

        //changing
        [ResponseType(typeof(TasaMoneda))]
        [Route("api/Monedas/{Simbolo}/Tasa")]
        [HttpGet]
        public async Task<IHttpActionResult> GetTasaMoneda(string Simbolo)
        {
            try
            {
                if (db.Monedas.Count(x => x.Simbolo.Equals(Simbolo)) == 0)
                {
                    return NotFound();
                }

                var TasasFecha = db.TasasMonedas.Where(x => x.Moneda.Simbolo.Equals(Simbolo)).OrderByDescending(x => x.Fecha);
                var Tasa = TasasFecha.Count() > 0 ? await TasasFecha.Include(m => m.Moneda).FirstAsync(): null;
                if (Tasa == null)
                {
                    return NotFound();
                }

                return Ok(Tasa);
            }
            catch(Exception e)
            {
                return InternalServerError(e);
            }
            
        }

        // PUT: api/Tasa/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTasaMoneda(int id, TasaMoneda TasaMoneda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != TasaMoneda.Id)
            {
                return BadRequest();
            }

            db.Entry(TasaMoneda).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TasaMonedaExists(id))
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

        // POST: api/Tasa
        [ResponseType(typeof(TasaMoneda))]
        public async Task<IHttpActionResult> PostTasaMoneda(TasaMoneda Tasa)
        {
            //agregar Fecha y Activar
            try
            {
                Tasa.Fecha = DateTime.Now;
                Tasa.Activa = true;
                var Monedas = db.Monedas.Where(x => x.Id == Tasa.MonedaId);

                if(Tasa.Valor <= 0 && Monedas.Count() == 0)
                {
                    return BadRequest();
                }

                Tasa.Moneda = Monedas.First();
                disableTasas(Tasa.Moneda.Id);    //desActivando todas demas Tasas
                db.TasasMonedas.Add(Tasa);
                await db.SaveChangesAsync();

                return Ok(Tasa);
            }catch(Exception e)
            {
                return InternalServerError();
            }
            
        }

        // DELETE: api/Tasa/5
        [ResponseType(typeof(TasaMoneda))]
        public async Task<IHttpActionResult> DeleteTasaMoneda(int id)
        {
            TasaMoneda TasaMoneda = await db.TasasMonedas.FindAsync(id);
            if (TasaMoneda == null)
            {
                return NotFound();
            }

            db.TasasMonedas.Remove(TasaMoneda);
            await db.SaveChangesAsync();

            return Ok(TasaMoneda);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TasaMonedaExists(int id)
        {
            return db.TasasMonedas.Count(e => e.Id == id) > 0;
        }

        private void disableTasas(int MonedaId)
        {
            var Tasas = db.TasasMonedas.Where(x => x.Moneda.Id ==(MonedaId));
            Tasas.ForEachAsync((TasaMoneda t) => {
                t.Activa = false;
                db.Entry(t).State = EntityState.Modified;
            });

            db.SaveChanges();
        }
    }
}