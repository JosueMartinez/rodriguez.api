using Rodriguez.Data.Models;
using Rodriguez.Repo;
using Rodriguez.Repo.Interfaces;
using System.Collections;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace rodriguez.api.Controllers
{
    public class MonedasController : ApiController
    {
        private readonly IRepository<Moneda> repo = null;

        public MonedasController()
        {
            repo = new Repository<Moneda>();
        }

        // GET: api/Monedas
        public IEnumerable GetMonedas()
        {
            return repo.Get();
        }

        // GET: api/Monedas/5
        [ResponseType(typeof(Moneda))]
        public IHttpActionResult GetMoneda(int id)
        {
            Moneda Moneda = repo.Get(id);
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

            repo.Update(Moneda);

            try
            {
                repo.Save();
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

            repo.Insert(Moneda);
            repo.Save();

            return CreatedAtRoute("DefaultApi", new { id = Moneda.Id }, Moneda);
        }

        // DELETE: api/Monedas/5
        [ResponseType(typeof(Moneda))]
        public IHttpActionResult DeleteMoneda(int id)
        {
            Moneda Moneda = repo.Get(id);
            if (Moneda == null)
            {
                return NotFound();
            }

            repo.Delete(id);
            repo.Save();

            return Ok(Moneda);
        }


        private bool MonedaExists(int id)
        {
            return repo.Get(id) != null;
        }
    }
}