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
        public IQueryable<Medida> Getmedidas()
        {
            return db.Medidas;
        }

        // GET: api/medidas/5
        [ResponseType(typeof(Medida))]
        public async Task<IHttpActionResult> Getmedida(int id)
        {
            Medida medida = await db.Medidas.FindAsync(id);
            if (medida == null)
            {
                return NotFound();
            }

            return Ok(medida);
        }

        // PUT: api/medidas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putmedida(int id, Medida medida)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != medida.Id)
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
        [ResponseType(typeof(Medida))]
        public async Task<IHttpActionResult> Postmedida(Medida medida)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Medidas.Add(medida);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = medida.Id }, medida);
        }

        // DELETE: api/medidas/5
        [ResponseType(typeof(Medida))]
        public async Task<IHttpActionResult> Deletemedida(int id)
        {
            Medida medida = await db.Medidas.FindAsync(id);
            if (medida == null)
            {
                return NotFound();
            }

            db.Medidas.Remove(medida);
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
            return db.Medidas.Count(e => e.Id == id) > 0;
        }
    }
}