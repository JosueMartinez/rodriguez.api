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
        public IQueryable<ListaCompra> Getlistacompras()
        {
            return db.Listas.Include("productosLista.producto.categoria");
        }

        // GET: api/listacompra/5
        [ResponseType(typeof(ListaCompra))]
        public async Task<IHttpActionResult> Getlistacompra(int id)
        {
            //var listas = db.listacompras.Where(x => x.id == id).Include(y => y.productosLista);
            var listas = db.Listas.Where(x => x.Id == id).Include("productosLista.producto.categoria");

            if (listas.Count() > 0)
            {
                return NotFound();
            }

            ListaCompra listacompra = await listas.FirstAsync();
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
            Cliente cliente = await db.Clientes.FindAsync(idCliente);
            if (cliente == null)
            {
                return NotFound();
            }

            var listas = db.Listas.Where(x => x.ClienteId == idCliente).Include("productosLista.producto.categoria");//.Include("producto.categoria");
            return Ok(listas);
        }

        // PUT: api/listacompra/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putlistacompra(int id, ListaCompra listacompra)
        { 
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != listacompra.Id)
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
        [ResponseType(typeof(ListaCompra))]
        public async Task<IHttpActionResult> Postlistacompra(ListaCompra listacompra)
        {
            listacompra.FechaCreacion = DateTime.Now;
            listacompra.FechaUltimaModificacion = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            db.Listas.Add(listacompra);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = listacompra.Id }, listacompra);
        }

        [HttpPut]
        [Route("api/listacompra/{listaId}/productos")]
        public async Task<IHttpActionResult> Putlistacompra(int listaId, ICollection<ListaProducto> productosNuevos)
        {
            ListaCompra listaExistente = await db.Listas.Where(x => x.Id == listaId).Include(x => x.Productos).FirstAsync();
            if(listaExistente != null)
            {
                var productosListaExistente = listaExistente.Productos.ToList();
                productosNuevos.ToList().ForEach(x =>
                {
                    //tomando en cuenta si es un producto ya en la lista o uno a agregar
                    var producto = productosListaExistente.Where(y => y.ProductoId == x.Id).Count() > 0?
                                    productosListaExistente.Where(y => y.ProductoId == x.Id).First():
                                    x;

                    if (listaExistente.containsProduct(producto.ProductoId)) {
                        producto.Cantidad = x.Cantidad;
                    }
                    else
                    {
                        listaExistente.Productos.Add(producto);
                    }
                });

            }
            listaExistente.FechaUltimaModificacion = DateTime.Now;
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
        [ResponseType(typeof(ListaCompra))]
        public async Task<IHttpActionResult> Deletelistacompra(int id)
        {
            ListaCompra listacompra = await db.Listas.FindAsync(id);
            if (listacompra == null)
            {
                return NotFound();
            }

            db.Listas.Remove(listacompra);
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
            return db.Listas.Count(e => e.Id == id) > 0;
        }


    }
}