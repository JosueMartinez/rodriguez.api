using Rodriguez.Data.Models;
using Rodriguez.Services.Interfaces;
using System;
using System.Collections;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using static Rodriguez.Data.Utils.Constants;

namespace rodriguez.api.Controllers
{
    [Authorize]
    public class BonosController : ApiController
    {
        private readonly IBonoService _bonoService;

        public BonosController(IBonoService bonoService)
        {
            _bonoService = bonoService;
        }

        // GET: api/Bonos
        public IEnumerable GetBonos()
        {            
            return _bonoService.Get(EstadosBonos.Comprado);
        }

        [Route("api/BonosPagados")]
        [HttpGet]
        public IEnumerable GetBonosPagados()
        {
            return _bonoService.Get(EstadosBonos.Cobrado);
        }

        // GET: api/Bonos/5
        [ResponseType(typeof(Bono))]
        public IHttpActionResult GetBono(int id)
        {
            Bono Bono = _bonoService.Get(id);
            if (Bono == null)
            {
                return NotFound();
            }

            return Ok(Bono);
        }

        // GET: api/Cliente/1/Bonos
        [ResponseType(typeof(Bono))]
        [Route("api/Cliente/{ClienteId}/Bonos")]
        [HttpGet]
        public IEnumerable GetBonoCliente(int ClienteId)
        {
            return _bonoService.GetBonosCliente(ClienteId);
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