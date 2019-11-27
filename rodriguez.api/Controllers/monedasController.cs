using Rodriguez.Data.Models;
using Rodriguez.Repo;
using Rodriguez.Repo.Interfaces;
using System.Collections;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace rodriguez.api.Controllers
{
    public class MonedasController : ApiController
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();

        public MonedasController()
        {
        }

        // GET: api/Monedas
        public IEnumerable GetMonedas()
        {
            return unitOfWork.Monedas.Get();
        }

        // GET: api/Monedas/5
        [ResponseType(typeof(Moneda))]
        public IHttpActionResult GetMoneda(int id)
        {
            Moneda Moneda = unitOfWork.Monedas.Get(id);
            if (Moneda == null)
            {
                return NotFound();
            }

            return Ok(Moneda);
        }

        // PUT: api/Monedas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMoneda(int id, Moneda Moneda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Moneda.Id)
            {
                return BadRequest();
            }

            unitOfWork.Monedas.Update(Moneda);

            try
            {
                unitOfWork.Commit();
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
        public IHttpActionResult PostMoneda(Moneda Moneda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.Monedas.Insert(Moneda);
            unitOfWork.Commit();

            return CreatedAtRoute("DefaultApi", new { id = Moneda.Id }, Moneda);
        }

        // DELETE: api/Monedas/5
        [ResponseType(typeof(Moneda))]
        public IHttpActionResult DeleteMoneda(int id)
        {
            Moneda Moneda = unitOfWork.Monedas.Get(id);
            if (Moneda == null)
            {
                return NotFound();
            }

            unitOfWork.Monedas.Delete(id);
            unitOfWork.Commit();

            return Ok(Moneda);
        }


        private bool MonedaExists(int id)
        {
            return unitOfWork.Monedas.Get(id) != null;
        }
    }
}