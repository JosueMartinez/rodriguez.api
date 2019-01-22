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
using rodriguez.api.Clases;
using Rodriguez.Data.Models;

namespace rodriguez.api.Controllers
{
    public class ListaCompraController : ApiController
    {
        private RodriguezModel db = new RodriguezModel();

        // GET: api/ListaCompra
        public IQueryable<ListaCompra> GetListaCompras()
        {
            return db.Listascompra.Include("ProductosLista.Producto.Categoria");
        }

        // GET: api/ListaCompra/5
        [ResponseType(typeof(ListaCompra))]
        public async Task<IHttpActionResult> GetListaCompra(int id)
        {
            //var listas = db.Listascompra.Where(x => x.Id == id).Include(y => y.ProductosLista);
            var listas = db.Listascompra.Where(x => x.Id == id).Include("ProductosLista.Producto.Categoria");

            if (listas.Count() > 0)
            {
                return NotFound();
            }

            ListaCompra ListaCompra = await listas.FirstAsync();
            if (ListaCompra == null)
            {
                return NotFound();
            }

            return Ok(ListaCompra);
        }

        [HttpGet]
        [Route("api/Cliente/{idCliente}/listas")]
        public async Task<IHttpActionResult> listasCliente(int idCliente)
        {
            Cliente Cliente = await db.Clientes.FindAsync(idCliente);
            if (Cliente == null)
            {
                return NotFound();
            }

            var listas = db.Listascompra.Where(x => x.ClienteId == idCliente).Include("ProductosLista.Producto.Categoria");//.Include("Producto.Categoria");
            return Ok(listas);
        }

        // PUT: api/ListaCompra/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutListaCompra(int id, ListaCompra ListaCompra)
        { 
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ListaCompra.Id)
            {
                return BadRequest();
            }

            db.Entry(ListaCompra).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListaCompraExists(id))
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

        // POST: api/ListaCompra
        [ResponseType(typeof(ListaCompra))]
        public async Task<IHttpActionResult> PostListaCompra(ListaCompra ListaCompra)
        {
            ListaCompra.FechaCreacion = DateTime.Now;
            ListaCompra.FechaUltimaModificacion = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            db.Listascompra.Add(ListaCompra);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = ListaCompra.Id }, ListaCompra);
        }

        [HttpPut]
        [Route("api/ListaCompra/{listaId}/Productos")]
        public async Task<IHttpActionResult> PutListaCompra(int listaId, ICollection<ListaCompraProducto> ProductosNuevos)
        {
            ListaCompra listaExistente = await db.Listascompra.Where(x => x.Id == listaId).Include(x => x.ProductosLista).FirstAsync();
            if(listaExistente != null)
            {
                var ProductosListaExistente = listaExistente.ProductosLista.ToList();
                ProductosNuevos.ToList().ForEach(x =>
                {
                    //tomando en cuenta si es un Producto ya en la lista o uno a agregar
                    var Producto = ProductosListaExistente.Where(y => y.ProductoId == x.ProductoId).Count() > 0?
                                    ProductosListaExistente.Where(y => y.ProductoId == x.ProductoId).First():
                                    x;

                    if (listaExistente.containsProduct(Producto.ProductoId)) {
                        Producto.Cantidad = x.Cantidad;
                    }
                    else
                    {
                        listaExistente.ProductosLista.Add(Producto);
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


        // DELETE: api/ListaCompra/5
        [ResponseType(typeof(ListaCompra))]
        public async Task<IHttpActionResult> DeleteListaCompra(int id)
        {
            ListaCompra ListaCompra = await db.Listascompra.FindAsync(id);
            if (ListaCompra == null)
            {
                return NotFound();
            }

            db.Listascompra.Remove(ListaCompra);
            await db.SaveChangesAsync();

            return Ok(ListaCompra);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ListaCompraExists(int id)
        {
            return db.Listascompra.Count(e => e.Id == id) > 0;
        }


    }
}