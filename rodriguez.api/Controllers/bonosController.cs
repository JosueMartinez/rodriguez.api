using AutoMapper;
using Rodriguez.Data.DTOs;
using Rodriguez.Data.Models;
using Rodriguez.Data.Utils;
using Rodriguez.Services.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace rodriguez.api.Controllers
{
    [Authorize]
    public class BonosController : ApiController
    {
        private readonly IBonoService _bonoService;
        private readonly IMapper _Mapper;

        public BonosController(IBonoService bonoService, IMapper mapper)
        {
            _bonoService = bonoService;
            _Mapper = mapper;
        }

        // GET: api/Bonos
        public IEnumerable GetBonos()
        { 
            var bonos = _bonoService.Get(EstadosBonos.Comprado);

            return _Mapper.Map<IEnumerable<BonoDetailDto>>(bonos);
        }

        [Route("api/BonosPagados")]
        [HttpGet]
        public IEnumerable GetBonosPagados()
        {
            var bonos = _bonoService.Get(EstadosBonos.Cobrado);

            return _Mapper.Map<IEnumerable<BonoDetailDto>>(bonos);
        }

        // GET: api/Bonos/5
        [ResponseType(typeof(Bono))]
        public IHttpActionResult GetBono(int id)
        {
            var bono = _bonoService.Get(id);
            if (bono == null)
            {
                return NotFound();
            }

            return Ok(_Mapper.Map<BonoDetailDto>(bono));
        }

        // GET: api/Cliente/1/Bonos
        [ResponseType(typeof(Bono))]
        [Route("api/Cliente/{ClienteId}/Bonos")]
        [HttpGet]
        public IEnumerable GetBonoCliente(int ClienteId)
        {
            var bonos = _bonoService.GetBonosCliente(ClienteId);
            return _Mapper.Map<IEnumerable<BonoDetailDto>>(bonos);
        }

        // PUT: api/Bonos/5/pagar
        [ResponseType(typeof(void))]
        [Route("api/Bonos/{BonoId}/pagar")]
        [HttpPut]
        public IHttpActionResult PutBono(int BonoId)
        {
            try
            {
                _bonoService.PagarBono(BonoId);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        // POST: api/Bonos
        [ResponseType(typeof(Bono))]
        public IHttpActionResult PostBono(Bono Bono)
        {
            try
            {
                _bonoService.AddBono(Bono);
                return Ok(Bono);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

        }

        // DELETE: api/Bonos/5
        [ResponseType(typeof(Bono))]
        public IHttpActionResult DeleteBono(int id)
        {
            Bono Bono = _bonoService.Get(id);
            if (Bono == null)
            {
                return NotFound();
            }

            _bonoService.DeleteBono(id);

            return Ok(Bono);
        }

    }
}