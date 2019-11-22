using Rodriguez.Data.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace rodriguez.api.Controllers
{
    public class MedidasController : ApiController
    {
        private RodriguezModel db = new RodriguezModel();

        // GET: api/Medidas
        public IQueryable<Medida> GetMedidas()
        {
            return db.Medidas;
        }

        // GET: api/Medidas/5
        [ResponseType(typeof(Medida))]
        public async Task<IHttpActionResult> GetMedida(int id)
        {
            Medida Medida = await db.Medidas.FindAsync(id);
            if (Medida == null)
            {
                return NotFound();
            }

            return Ok(Medida);
        }

        // PUT: api/Medidas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMedida(int id, Medida Medida)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Medida.Id)
            {
                return BadRequest();
            }

            db.Entry(Medida).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedidaExists(id))
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

        // POST: api/Medidas
        [ResponseType(typeof(Medida))]
        public async Task<IHttpActionResult> PostMedida(Medida Medida)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Medidas.Add(Medida);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = Medida.Id }, Medida);
        }

        // DELETE: api/Medidas/5
        [ResponseType(typeof(Medida))]
        public async Task<IHttpActionResult> DeleteMedida(int id)
        {
            Medida Medida = await db.Medidas.FindAsync(id);
            if (Medida == null)
            {
                return NotFound();
            }

            db.Medidas.Remove(Medida);
            await db.SaveChangesAsync();

            return Ok(Medida);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public bool MedidaExists(int id)
        {
            return db.Medidas.Count(e => e.Id == id) > 0;
        }
    }
}