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
        public IQueryable<bono> Getbonos()
        {
            return db.bonos.Include(p => p.cliente)
                        .Include(p => p.tasa)
                        .Include("tasa.moneda")
                        .Include(p => p.estadobono)
                        .Where(p => p.estadobono.descripcion.ToLower().Equals("comprado"))
                        .OrderByDescending(x => x.fechaCompra);
        }

        [Route("api/bonosPagados")]
        [HttpGet]
        public IQueryable<bono> GetbonosPagados()
        {
            return db.bonos.Include(p => p.cliente)
                        .Include(p => p.tasa)
                        .Include("tasa.moneda")
                        .Include(p => p.estadobono)
                        .Where(p => p.estadobono.descripcion.ToLower().Equals("cobrado"))
                        .OrderByDescending(x => x.fechaCompra);
        }

        // GET: api/bonos/5
        [ResponseType(typeof(bono))]
        public async Task<IHttpActionResult> Getbono(int id)
        {
            bono bono = await db.bonos.Include(p => p.cliente).Include(p => p.tasa).Include("tasa.moneda").Include(p => p.estadobono).SingleOrDefaultAsync(i => i.id == id);
            if (bono == null)
            {
                return NotFound();
            }

            return Ok(bono);
        }

        // GET: api/cliente/1/bonos
        [ResponseType(typeof(bono))]
        [Route("api/cliente/{clienteId}/bonos")]
        [HttpGet]
        public IQueryable<bono> GetBonoCliente(int clienteId)
        {
            return db.bonos.Where(x => x.clienteId == clienteId)
                .Include(p => p.cliente).Include(p => p.tasa)
                .Include("tasa.moneda")
                .Include(p => p.estadobono)
                .OrderByDescending(x => x.fechaCompra); ;
        }

        // PUT: api/bonos/5/pagar
        [ResponseType(typeof(void))]
        [Route("api/bonos/{bonoId}/pagar")]
        [HttpPut]
        public async Task<IHttpActionResult> Putbono(int bonoId)
        {
            bono bono = await db.bonos.FindAsync(bonoId);
            int idCobrado = db.estadobonos.Where(x => x.descripcion.Equals("Cobrado")).FirstOrDefault().id;

            if (bono == null)
            {
                return NotFound();
            }

            //creacion movimiento historial
            historialbono hist = new historialbono();
            hist.bonoId = bono.id;
            hist.estadoBonoId = idCobrado;
            hist.fechaEntradaEstado = DateTime.Now;

            bono.estadoBonoId = idCobrado;
            if(bono.estadoBonoId != 0)
            {
                db.Entry(bono).State = EntityState.Modified;

                try
                {
                    db.historialbonos.Add(hist);
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
        [ResponseType(typeof(bono))]
        public async Task<IHttpActionResult> Postbono(bono bono)
        {
            try
            {
                //ESTADO BONO SIEMPRE COMPRADO
                bono.estadoBonoId =  db.estadobonos.Where(x => x.descripcion.Equals("comprado")).FirstOrDefault().id;
                //FECHA COMPRA NOW
                bono.fechaCompra = DateTime.Now;

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                db.bonos.Add(bono);
                await db.SaveChangesAsync();
                return Ok(bono);
                //return CreatedAtRoute("DefaultApi", new { id = bono.id }, bono);
            }catch(Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.ExpectationFailed);
            }
            
        }

        // DELETE: api/bonos/5
        [ResponseType(typeof(bono))]
        public async Task<IHttpActionResult> Deletebono(int id)
        {
            bono bono = await db.bonos.FindAsync(id);
            if (bono == null)
            {
                return NotFound();
            }

            db.bonos.Remove(bono);
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
            return db.bonos.Count(e => e.id == id) > 0;
        }
    }
}