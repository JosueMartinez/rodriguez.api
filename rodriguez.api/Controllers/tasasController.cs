using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Rodriguez.Data.Models;
using System.Collections;
using Rodriguez.Repo.Interfaces;
using AutoMapper;
using Rodriguez.Data.DTOs;
using System.Collections.Generic;

namespace rodriguez.api.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    [Authorize]
    public class TasasController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _Mapper;

        public TasasController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _Mapper = mapper;
        }

        // GET: api/Tasas
        public IEnumerable GetTasasMonedas()
        {
            var tasas = _unitOfWork.TasasCustom.GetAll();

            return _Mapper.Map<IEnumerable<TasaDto>>(tasas);
        }

        [ResponseType(typeof(TasaMoneda))]
        [Route("api/Monedas/{MonedaId:int}/historial")]
        [HttpGet]
        public IEnumerable GetHistorial(int MonedaId)
        {
            return _unitOfWork.TasasCustom.GetHistorial(MonedaId);
        }
        
        [ResponseType(typeof(TasaMoneda))]
        [Route("api/Monedas/{MonedaId:int}/Tasa")]
        [HttpGet]
        public IHttpActionResult GetTasaMoneda(int MonedaId)
        {
            var Tasa = _unitOfWork.Tasas.Get(MonedaId);
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
                var Tasa = _unitOfWork.TasasCustom.GetTasaMoneda(Simbolo);
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
                _unitOfWork.Tasas.Update(TasaMoneda);
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
                var Monedas = _unitOfWork.Monedas.Get().Where(x => x.Id == Tasa.Moneda.Id);

                if (Tasa.Valor <= 0 && Monedas.Count() == 0)
                {
                    return BadRequest();
                }

                Tasa.Moneda = Monedas.First();
                disableTasas(Tasa.Moneda.Id);    //desActivando todas demas Tasas
                _unitOfWork.Tasas.Insert(Tasa);
                _unitOfWork.Commit();

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
            TasaMoneda TasaMoneda = _unitOfWork.Tasas.Get(id);
            if (TasaMoneda == null)
            {
                return NotFound();
            }

            _unitOfWork.Tasas.Delete(id);
            _unitOfWork.Commit();

            return Ok(TasaMoneda);
        }

        private bool TasaMonedaExists(int id)
        {
            return _unitOfWork.Tasas.Get(id) != null; 
        }

        private void disableTasas(int MonedaId)
        {
            _unitOfWork.TasasCustom.DisableTasa(MonedaId);
            _unitOfWork.Commit();
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}