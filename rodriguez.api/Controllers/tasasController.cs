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
                
                if (db.tasasmonedas.Count(x => x.moneda.id == moneda.id && x.activo == true) > 0)
                {
                    tasamoneda tasamoneda = await db.tasasmonedas.FirstAsync(x => x.moneda.id == moneda.id && x.activo == true);
                    if (tasamoneda == null)
                    {
                        return NotFound();
                    }

                    return Ok(tasamoneda);
                }
                else
                {
                    var tasasFecha = db.tasasmonedas.Where(x => x.moneda.id == monedaId).OrderByDescending(x => x.fecha);
                    var tasa = tasasFecha.FirstAsync();
                    if (tasa == null)
                    {
                        return NotFound();
                    }

                    return Ok(tasa);
                }
            }
        }

        [ResponseType(typeof(tasamoneda))]
        [Route("api/monedas/{simbolo}/tasa")]
        [HttpGet]
        public async Task<IHttpActionResult> Gettasamoneda(string simbolo)
        {
            try
            {
                if (db.tasasmonedas.Count(x => x.moneda.simbolo.Equals(simbolo)) > 0)
                {
                    tasamoneda tasa = await db.tasasmonedas.Where(x => x.moneda.simbolo.Equals(simbolo)).FirstAsync();
                    if (tasa == null)
                    {
                        return NotFound();
                    }
                    return Ok(tasa);

                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception e)
            {
                return InternalServerError();
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
            //desactivar demas tasas de esta moneda
            try
            {
                var tasas = db.tasasmonedas.Where(x => x.monedaId == tasa.monedaId).ToList();
                tasas.ForEach(x => x.activo = false);
                db.SaveChanges();
            }catch(Exception e)
            {
                return InternalServerError();
            }
            //Fin desactivacion

            //agregar fecha y activar
            try
            {
                tasa.fecha = DateTime.Now;
                tasa.activo = true;
                var monedas = db.monedas.Where(x => x.id == tasa.monedaId);

                if(tasa.valor <= 0 && monedas.Count() == 0)
                {
                    return BadRequest();
                }

                tasa.moneda = monedas.First();
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
    }
}