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
using System.Web.Http.Cors;
using Rodriguez.Data.Models;
using Rodriguez.Repo;
using System.Collections;
using Rodriguez.Repo.Interfaces;

namespace rodriguez.api.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    //[Authorize]
    public class TasasController : ApiController
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();

        public TasasController()
        {
        }

        // GET: api/Tasas
        public IEnumerable GetTasasMonedas()
        {
            return unitOfWork.Tasas.Get();
        }

        [ResponseType(typeof(TasaMoneda))]
        [Route("api/Monedas/{MonedaId:int}/historial")]
        [HttpGet]
        public IEnumerable GetHistorial(int MonedaId)
        {
            return unitOfWork.TasasCustom.GetHistorial(MonedaId);
        }
        
        [ResponseType(typeof(TasaMoneda))]
        [Route("api/Monedas/{MonedaId:int}/Tasa")]
        [HttpGet]
        public IHttpActionResult GetTasaMoneda(int MonedaId)
        {
            var Tasa = unitOfWork.Tasas.Get(MonedaId);
            if (Tasa == null)
            {
                return NotFound();
            }

            return Ok(Tasa);
        }

        //changing
        [ResponseType(typeof(TasaMoneda))]
        [Route("api/Monedas/{Simbolo}/Tasa")]
        [HttpGet]
        public IHttpActionResult GetTasaMoneda(string Simbolo)
        {
            try
            {
                var Tasa = unitOfWork.TasasCustom.GetTasaMoneda(Simbolo);
                if (Tasa == null)
                {
                    return NotFound();
                }

                return Ok(Tasa);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

        }

        // PUT: api/Tasa/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTasaMoneda(int id, TasaMoneda TasaMoneda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != TasaMoneda.Id)
            {
                return BadRequest();
            }

            try
            {
                unitOfWork.Tasas.Update(TasaMoneda);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TasaMonedaExists(id))
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

        // POST: api/Tasa
        [ResponseType(typeof(TasaMoneda))]
        public IHttpActionResult PostTasaMoneda(TasaMoneda Tasa)
        {
            //agregar Fecha y Activar
            try
            {
                Tasa.Fecha = DateTime.Now;
                Tasa.Activa = true;
                var Monedas = unitOfWork.Monedas.Get().Where(x => x.Id == Tasa.Moneda.Id);

                if (Tasa.Valor <= 0 && Monedas.Count() == 0)
                {
                    return BadRequest();
                }

                Tasa.Moneda = Monedas.First();
                disableTasas(Tasa.Moneda.Id);    //desActivando todas demas Tasas
                unitOfWork.Tasas.Insert(Tasa);
                unitOfWork.Commit();

                return Ok(Tasa);
            }
            catch (Exception e)
            {
                return InternalServerError();
            }

        }

        // DELETE: api/Tasa/5
        [ResponseType(typeof(TasaMoneda))]
        public IHttpActionResult DeleteTasaMoneda(int id)
        {
            TasaMoneda TasaMoneda = unitOfWork.Tasas.Get(id);
            if (TasaMoneda == null)
            {
                return NotFound();
            }

            unitOfWork.Tasas.Delete(id);
            unitOfWork.Commit();

            return Ok(TasaMoneda);
        }

        private bool TasaMonedaExists(int id)
        {
            return unitOfWork.Tasas.Get(id) != null; 
        }

        private void disableTasas(int MonedaId)
        {
            unitOfWork.TasasCustom.DisableTasa(MonedaId);
            unitOfWork.Commit();
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}