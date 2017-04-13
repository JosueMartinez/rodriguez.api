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
using rodriguez.api.Clases;

namespace rodriguez.api.Controllers
{
    public class listacompraController : ApiController
    {
        private RodriguezModel db = new RodriguezModel();

        // GET: api/listacompra
        public IQueryable<listacompra> Getlistacompras()
        {
            return db.listacompras.Include(x => x.productosLista);
        }

        // GET: api/listacompra/5
        [ResponseType(typeof(listacompra))]
        public async Task<IHttpActionResult> Getlistacompra(int id)
        {
            //var listas = db.listacompras.Where(x => x.id == id).Include(y => y.productosLista);
            var listas = db.listacompras.Where(x => x.id == id).Include("productosLista.producto");

            if (listas.Count() > 0)
            {
                return NotFound();
            }

            listacompra listacompra = await listas.FirstAsync();
            if (listacompra == null)
            {
                return NotFound();
            }

            return Ok(listacompra);
        }

        [HttpGet]
        [Route("api/cliente/{idCliente}/listas")]
        public async Task<IHttpActionResult> listasCliente(int idCliente)
        {
            cliente cliente = await db.clientes.FindAsync(idCliente);
            if (cliente == null)
            {
                return NotFound();
            }

            var listas =  db.listacompras.Where(x => x.clienteId == idCliente).Include("productosLista.producto");
            return Ok(listas);
        }

        // PUT: api/listacompra/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putlistacompra(int id, listacompra listacompra)
        { 
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != listacompra.id)
            {
                return BadRequest();
            }

            db.Entry(listacompra).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!listacompraExists(id))
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

        // POST: api/listacompra
        [ResponseType(typeof(listacompra))]
        public async Task<IHttpActionResult> Postlistacompra(listacompra listacompra)
        {
            listacompra.fechaCreacion = DateTime.Now;
            listacompra.fechaUltimaModificacion = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            db.listacompras.Add(listacompra);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = listacompra.id }, listacompra);
        }

        [HttpPut]
        [Route("api/listacompra/{listaId}/productos")]
        public async Task<IHttpActionResult> Putlistacompra(int listaId, ICollection<listacompraproducto> productosNuevos)
        {
            listacompra listaExistente = await db.listacompras.Where(x => x.id == listaId).Include(x => x.productosLista).FirstAsync();
            if(listaExistente != null)
            {
                var productosListaExistente = listaExistente.productosLista.ToList();
                productosNuevos.ToList().ForEach(x =>
                {
                    //tomando en cuenta si es un producto ya en la lista o uno a agregar
                    var producto = productosListaExistente.Where(y => y.productoId == x.productoId).Count() > 0?
                                    productosListaExistente.Where(y => y.productoId == x.productoId).First():
                                    x;

                    if (listaExistente.containsProduct(producto.productoId)) {
                        producto.cantidad = x.cantidad;
                    }
                    else
                    {
                        listaExistente.productosLista.Add(producto);
                    }
                });

            }
            listaExistente.fechaUltimaModificacion = DateTime.Now;
            db.Entry(listaExistente).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
                return Ok();
            }catch(Exception e)
            {
                return InternalServerError(e);
            }

        }


        // DELETE: api/listacompra/5
        [ResponseType(typeof(listacompra))]
        public async Task<IHttpActionResult> Deletelistacompra(int id)
        {
            listacompra listacompra = await db.listacompras.FindAsync(id);
            if (listacompra == null)
            {
                return NotFound();
            }

            db.listacompras.Remove(listacompra);
            await db.SaveChangesAsync();

            return Ok(listacompra);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool listacompraExists(int id)
        {
            return db.listacompras.Count(e => e.id == id) > 0;
        }


    }
}