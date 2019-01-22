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
    [Authorize]
    public class BonosController : ApiController
    {
        private RodriguezModel db = new RodriguezModel();

        // GET: api/Bonos
        public IQueryable<Bono> GetBonos()
        {
            return db.Bonos.Include(p => p.Cliente)
                        .Include(p => p.Tasa)
                        .Include("Tasa.Moneda")
                        .Include(p => p.EstadoBono)
                        .Where(p => p.EstadoBono.Descripcion.ToLower().Equals("comprado"))
                        .OrderByDescending(x => x.FechaCompra);
        }

        [Route("api/BonosPagados")]
        [HttpGet]
        public IQueryable<Bono> GetBonosPagados()
        {
            return db.Bonos.Include(p => p.Cliente)
                        .Include(p => p.Tasa)
                        .Include("Tasa.Moneda")
                        .Include(p => p.EstadoBono)
                        .Where(p => p.EstadoBono.Descripcion.ToLower().Equals("cobrado"))
                        .OrderByDescending(x => x.FechaCompra);
        }

        // GET: api/Bonos/5
        [ResponseType(typeof(Bono))]
        public async Task<IHttpActionResult> GetBono(int id)
        {
            Bono Bono = await db.Bonos.Include(p => p.Cliente).Include(p => p.Tasa).Include("Tasa.Moneda").Include(p => p.EstadoBono).SingleOrDefaultAsync(i => i.Id == id);
            if (Bono == null)
            {
                return NotFound();
            }

            return Ok(Bono);
        }

        // GET: api/Cliente/1/Bonos
        [ResponseType(typeof(Bono))]
        [Route("api/Cliente/{ClienteId}/Bonos")]
        [HttpGet]
        public IQueryable<Bono> GetBonoCliente(int ClienteId)
        {
            return db.Bonos.Where(x => x.ClienteId == ClienteId)
                .Include(p => p.Cliente).Include(p => p.Tasa)
                .Include("Tasa.Moneda")
                .Include(p => p.EstadoBono)
                .OrderByDescending(x => x.FechaCompra); ;
        }

        // PUT: api/Bonos/5/pagar
        [ResponseType(typeof(void))]
        [Route("api/Bonos/{BonoId}/pagar")]
        [HttpPut]
        public async Task<IHttpActionResult> PutBono(int BonoId)
        {
            Bono Bono = await db.Bonos.FindAsync(BonoId);
            int idCobrado = db.EstadosBonos.Where(x => x.Descripcion.Equals("Cobrado")).FirstOrDefault().Id;

            if (Bono == null)
            {
                return NotFound();
            }

            //creacion movimiento historial
            HistorialBono hist = new HistorialBono();
            hist.BonoId = Bono.Id;
            hist.EstadoBonoId = idCobrado;
            hist.FechaEntradaEstado = DateTime.Now;

            Bono.EstadoBonoId = idCobrado;
            if(Bono.EstadoBonoId != 0)
            {
                db.Entry(Bono).State = EntityState.Modified;

                try
                {
                    db.HistorialBonos.Add(hist);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }

                return StatusCode(HttpStatusCode.NoContent);
            }

            return BadRequest();
            
        }

        // POST: api/Bonos
        [ResponseType(typeof(Bono))]
        public async Task<IHttpActionResult> PostBono(Bono Bono)
        {
            try
            {
                //ESTADO Bono SIEMPRE COMPRADO
                Bono.EstadoBonoId =  db.EstadosBonos.Where(x => x.Descripcion.Equals("comprado")).FirstOrDefault().Id;
                //Fecha COMPRA NOW
                Bono.FechaCompra = DateTime.Now;

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                db.Bonos.Add(Bono);
                await db.SaveChangesAsync();
                return Ok(Bono);
                //return CreatedAtRoute("DefaultApi", new { id = Bono.Id }, Bono);
            }catch(Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.ExpectationFailed);
            }
            
        }

        // DELETE: api/Bonos/5
        [ResponseType(typeof(Bono))]
        public async Task<IHttpActionResult> DeleteBono(int id)
        {
            Bono Bono = await db.Bonos.FindAsync(id);
            if (Bono == null)
            {
                return NotFound();
            }

            db.Bonos.Remove(Bono);
            await db.SaveChangesAsync();

            return Ok(Bono);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BonoExists(int id)
        {
            return db.Bonos.Count(e => e.Id == id) > 0;
        }
    }
}