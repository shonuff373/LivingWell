using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LivingWellMVC.Controllers.Api
{
    public class ErrorController : ApiController
    {
        // GET: api/Error
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Error/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Error
        public string Post([FromBody]string value)
        {
            return "There was an error with you submission. Please call Living Well Services for further support.";
        }

        // PUT: api/Error/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Error/5
        public void Delete(int id)
        {
        }
    }
}
