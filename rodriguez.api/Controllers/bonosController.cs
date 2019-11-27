using Rodriguez.Data.Models;
using Rodriguez.Repo;
using System;
using System.Collections;
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
        private readonly UnitOfWork unitOfWork = new UnitOfWork();

        public BonosController()
        {
        }

        // GET: api/Bonos
        public IEnumerable GetBonos()
        {
            return unitOfWork.Bonos.Get().Where(p => p.EstadoBono.Descripcion.ToLower().Equals("comprado"))
                        .OrderByDescending(x => x.FechaCompra);

            //return db.Bonos.Include(p => p.Cliente)
            //            .Include(p => p.Tasa)
            //            .Include("Tasa.Moneda")
            //            .Include(p => p.EstadoBono)
            //            .Where(p => p.EstadoBono.Descripcion.ToLower().Equals("comprado"))
            //            .OrderByDescending(x => x.FechaCompra);
        }

        [Route("api/BonosPagados")]
        [HttpGet]
        public IEnumerable GetBonosPagados()
        {
            return unitOfWork.Bonos.Get().Where(p => p.EstadoBono.Descripcion.ToLower().Equals("cobrado"))
                        .OrderByDescending(x => x.FechaCompra);
            //return db.Bonos.Include(p => p.Cliente)
            //            .Include(p => p.Tasa)
            //            .Include("Tasa.Moneda")
            //            .Include(p => p.EstadoBono)
            //            .Where(p => p.EstadoBono.Descripcion.ToLower().Equals("cobrado"))
            //            .OrderByDescending(x => x.FechaCompra);
        }

        // GET: api/Bonos/5
        [ResponseType(typeof(Bono))]
        public IHttpActionResult GetBono(int id)
        {
            Bono Bono = unitOfWork.Bonos.Get(id);//await db.Bonos.Include(p => p.Cliente).Include(p => p.Tasa).Include("Tasa.Moneda").Include(p => p.EstadoBono).SingleOrDefaultAsync(i => i.Id == id);
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
        public IEnumerable GetBonoCliente(int ClienteId)
        {
            return unitOfWork.Bonos.Get().Where(x => x.ClienteId == ClienteId).OrderByDescending(x => x.FechaCompra);
            //return db.Bonos.Where(x => x.ClienteId == ClienteId)
            //    .Include(p => p.Cliente).Include(p => p.Tasa)
            //    .Include("Tasa.Moneda")
            //    .Include(p => p.EstadoBono)
            //    .OrderByDescending(x => x.FechaCompra);
        }

        // PUT: api/Bonos/5/pagar
        [ResponseType(typeof(void))]
        [Route("api/Bonos/{BonoId}/pagar")]
        [HttpPut]
        public IHttpActionResult PutBono(int BonoId)
        {
            Bono Bono = unitOfWork.Bonos.Get(BonoId);
            int idCobrado = unitOfWork.Estados.Get().Where(x => x.Descripcion.Equals("Cobrado")).FirstOrDefault().Id;

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
            if (Bono.EstadoBonoId != 0)
            {
                unitOfWork.Bonos.Update(Bono);

                try
                {
                    unitOfWork.Historiales.Insert(hist);
                    unitOfWork.Commit();
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
        public IHttpActionResult PostBono(Bono Bono)
        {
            try
            {
                //ESTADO Bono SIEMPRE COMPRADO
                Bono.EstadoBonoId = unitOfWork.Estados.Get().Where(x => x.Descripcion.Equals("comprado")).FirstOrDefault().Id;
                //Fecha COMPRA NOW
                Bono.FechaCompra = DateTime.Now;

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                unitOfWork.Bonos.Insert(Bono);
                unitOfWork.Commit();
                return Ok(Bono);
                //return CreatedAtRoute("DefaultApi", new { id = Bono.Id }, Bono);
            }
            catch (Exception e)
            {
                return InternalServerError();
            }

        }

        // DELETE: api/Bonos/5
        [ResponseType(typeof(Bono))]
        public IHttpActionResult DeleteBono(int id)
        {
            Bono Bono = unitOfWork.Bonos.Get(id);
            if (Bono == null)
            {
                return NotFound();
            }

            unitOfWork.Bonos.Delete(id);
            unitOfWork.Commit();

            return Ok(Bono);
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        private bool BonoExists(int id)
        {
            return unitOfWork.Bonos.Get(id) != null;
        }
    }
}