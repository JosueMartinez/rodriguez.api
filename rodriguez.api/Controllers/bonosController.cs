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
        //private RodriguezModel db = new RodriguezModel();
        private readonly Repository<Bono> repo = null;
        //private readonly BonoRepository bonoRepo = null;
        private readonly Repository<EstadoBono> estadoRepo = null;
        private readonly Repository<HistorialBono> histRepo = null;

        public BonosController()
        {
            repo = new Repository<Bono>();
            //bonoRepo = new BonoRepository();
            estadoRepo = new Repository<EstadoBono>();
            histRepo = new Repository<HistorialBono>();
        }

        // GET: api/Bonos
        public IEnumerable GetBonos()
        {
            return repo.Get().Where(p => p.EstadoBono.Descripcion.ToLower().Equals("comprado"))
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
            return repo.Get().Where(p => p.EstadoBono.Descripcion.ToLower().Equals("cobrado"))
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
            Bono Bono = repo.Get(id);//await db.Bonos.Include(p => p.Cliente).Include(p => p.Tasa).Include("Tasa.Moneda").Include(p => p.EstadoBono).SingleOrDefaultAsync(i => i.Id == id);
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
            return repo.Get().Where(x => x.ClienteId == ClienteId).OrderByDescending(x => x.FechaCompra);
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
            Bono Bono = repo.Get(BonoId);
            int idCobrado = estadoRepo.Get().Where(x => x.Descripcion.Equals("Cobrado")).FirstOrDefault().Id;

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
                repo.Update(Bono);

                try
                {
                    histRepo.Insert(hist);
                    repo.Save();
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
                Bono.EstadoBonoId = estadoRepo.Get().Where(x => x.Descripcion.Equals("comprado")).FirstOrDefault().Id;
                //Fecha COMPRA NOW
                Bono.FechaCompra = DateTime.Now;

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                repo.Insert(Bono);
                repo.Save();
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
            Bono Bono = repo.Get(id);
            if (Bono == null)
            {
                return NotFound();
            }

            repo.Delete(id);
            repo.Save();

            return Ok(Bono);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BonoExists(int id)
        {
            return repo.Get(id) != null;
        }
    }
}