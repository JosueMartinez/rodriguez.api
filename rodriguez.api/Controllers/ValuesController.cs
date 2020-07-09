using Rodriguez.Common;
using System.Collections.Generic;
using System.Web.Http;

namespace rodriguez.api.Controllers
{
    //[EnableCors(origins:"*", headers:"*", methods:"*")]
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "VALUE", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return Utilidades.Capitalize("value");
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
