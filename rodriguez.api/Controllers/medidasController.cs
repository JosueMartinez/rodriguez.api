using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using rodriguez.api.Models;

namespace rodriguez.api.Controllers
{
    public class medidasController : ApiController
    {
        private RodriguezModel db = new RodriguezModel();

        // GET: api/medidas
        public IQueryable<medida> Getmedidas()
        {
            return db.medidas;
        }

        // GET: api/medidas/5
        [ResponseType(typeof(medida))]
        public async Task<IHttpActionResult> Getmedida(int id)
        {
            medida medida = await db.medidas.FindAsync(id);
            if (medida == null)
            {
                return NotFound();
            }

            return Ok(medida);
        }

        // PUT: api/medidas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putmedida(int id, medida medida)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != medida.id)
            {
                return BadRequest();
            }

            db.Entry(medida).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!medidaExists(id))
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

        // POST: api/medidas
        [ResponseType(typeof(medida))]
        public async Task<IHttpActionResult> Postmedida(medida medida)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.medidas.Add(medida);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = medida.id }, medida);
        }

        // DELETE: api/medidas/5
        [ResponseType(typeof(medida))]
        public async Task<IHttpActionResult> Deletemedida(int id)
        {
            medida medida = await db.medidas.FindAsync(id);
            if (medida == null)
            {
                return NotFound();
            }

            db.medidas.Remove(medida);
            await db.SaveChangesAsync();

            return Ok(medida);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public bool medidaExists(int id)
        {
            return db.medidas.Count(e => e.id == id) > 0;
        }
    }
}