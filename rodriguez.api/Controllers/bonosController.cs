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
    [Authorize]
    public class bonosController : ApiController
    {
        private RodriguezModel db = new RodriguezModel();

        // GET: api/bonos
        public IQueryable<Bono> Getbonos()
        {
            return db.Bonos.Include(p => p.Cliente)
                        .Include(p => p.Tasa)
                        .Include("tasa.Moneda")
                        .Include(p => p.Estado)
                        .Where(p => p.Estado.Descripcion.ToLower().Equals("comprado"))
                        .OrderByDescending(x => x.FechaCompra);
        }

        [Route("api/bonosPagados")]
        [HttpGet]
        public IQueryable<Bono> GetbonosPagados()
        {
            return db.Bonos.Include(p => p.Cliente)
                        .Include(p => p.Tasa)
                        .Include("Tasa.Moneda")
                        .Include(p => p.Estado)
                        .Where(p => p.Estado.Descripcion.ToLower().Equals("cobrado"))
                        .OrderByDescending(x => x.FechaCompra);
        }

        // GET: api/bonos/5
        [ResponseType(typeof(Bono))]
        public async Task<IHttpActionResult> Getbono(int id)
        {
            Bono bono = await db.Bonos.Include(p => p.Cliente).Include(p => p.Tasa).Include("Tasa.Moneda").Include(p => p.Estado).SingleOrDefaultAsync(i => i.Id == id);
            if (bono == null)
            {
                return NotFound();
            }

            return Ok(bono);
        }

        // GET: api/cliente/1/bonos
        [ResponseType(typeof(Bono))]
        [Route("api/cliente/{clienteId}/bonos")]
        [HttpGet]
        public IQueryable<Bono> GetBonoCliente(int clienteId)
        {
            return db.Bonos.Where(x => x.ClienteId == clienteId)
                .Include(p => p.Cliente).Include(p => p.Tasa)
                .Include("Tasa.Moneda")
                .Include(p => p.Estado)
                .OrderByDescending(x => x.FechaCompra); ;
        }

        // PUT: api/bonos/5/pagar
        [ResponseType(typeof(void))]
        [Route("api/bonos/{bonoId}/pagar")]
        [HttpPut]
        public async Task<IHttpActionResult> Putbono(int bonoId)
        {
            Bono bono = await db.Bonos.FindAsync(bonoId);
            int idCobrado = db.Estados.Where(x => x.Descripcion.Equals("Cobrado")).FirstOrDefault().Id;

            if (bono == null)
            {
                return NotFound();
            }

            //creacion movimiento historial
            HistorialBono hist = new HistorialBono();
            hist.BonoId = bono.Id;
            hist.EstadoBonoId = idCobrado;
            hist.FechaEntradaEstado = DateTime.Now;

            bono.EstadoBonoId = idCobrado;
            if(bono.EstadoBonoId != 0)
            {
                db.Entry(bono).State = EntityState.Modified;

                try
                {
                    db.Historial.Add(hist);
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

        // POST: api/bonos
        [ResponseType(typeof(Bono))]
        public async Task<IHttpActionResult> Postbono(Bono bono)
        {
            try
            {
                //ESTADO BONO SIEMPRE COMPRADO
                bono.EstadoBonoId =  db.Estados.Where(x => x.Descripcion.Equals("comprado")).FirstOrDefault().Id;
                //FECHA COMPRA NOW
                bono.FechaCompra = DateTime.Now;

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                db.Bonos.Add(bono);
                await db.SaveChangesAsync();
                return Ok(bono);
                //return CreatedAtRoute("DefaultApi", new { id = bono.id }, bono);
            }catch(Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.ExpectationFailed);
            }
            
        }

        // DELETE: api/bonos/5
        [ResponseType(typeof(Bono))]
        public async Task<IHttpActionResult> Deletebono(int id)
        {
            Bono bono = await db.Bonos.FindAsync(id);
            if (bono == null)
            {
                return NotFound();
            }

            db.Bonos.Remove(bono);
            await db.SaveChangesAsync();

            return Ok(bono);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool bonoExists(int id)
        {
            return db.Bonos.Count(e => e.Id == id) > 0;
        }
    }
}