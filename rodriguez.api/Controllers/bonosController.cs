﻿using System;
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

namespace rodriguez.api.Controllers
{
    public class bonosController : ApiController
    {
        private RodriguezModel db = new RodriguezModel();

        // GET: api/bonos
        public IQueryable<bono> Getbonos()
        {
            return db.bonos;
        }

        // GET: api/bonos/5
        [ResponseType(typeof(bono))]
        public async Task<IHttpActionResult> Getbono(int id)
        {
            bono bono = await db.bonos.FindAsync(id);
            if (bono == null)
            {
                return NotFound();
            }

            return Ok(bono);
        }

        // GET: api/cliente/1/bonos
        [ResponseType(typeof(bono))]
        [Route("api/cliente/{clienteId}/bonos")]
        [HttpGet]
        public IQueryable<bono> GetBonoCliente(int clienteId)
        {
            return db.bonos.Where(x => x.clienteId == clienteId).Include(p => p.cliente).Include(p => p.moneda);
        }

        // PUT: api/bonos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putbono(int id, bono bono)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bono.id)
            {
                return BadRequest();
            }

            db.Entry(bono).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!bonoExists(id))
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

        // POST: api/bonos
        [ResponseType(typeof(bono))]
        public async Task<IHttpActionResult> Postbono(bono bono)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.bonos.Add(bono);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = bono.id }, bono);
        }

        // DELETE: api/bonos/5
        [ResponseType(typeof(bono))]
        public async Task<IHttpActionResult> Deletebono(int id)
        {
            bono bono = await db.bonos.FindAsync(id);
            if (bono == null)
            {
                return NotFound();
            }

            db.bonos.Remove(bono);
            await db.SaveChangesAsync();

            return Ok(bono);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool bonoExists(int id)
        {
            return db.bonos.Count(e => e.id == id) > 0;
        }
    }
}