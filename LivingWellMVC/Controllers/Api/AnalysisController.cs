using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using LivingWellMVC.Models;

namespace LivingWellMVC.Controllers.Api
{
    public class AnalysisController : ApiController
    {
        // GET: api/Email
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Email/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Email
        [HttpPost]
        public void Post([FromBody]string firstName, 
            string lastName, string communityName, string addressOne,
            string addressTwo, string city, string state, string postalCode, string phone)
        {
            AnalysisTemplate form = new AnalysisTemplate();

            Status emailStatus = EmailService.ProcessEmail(form);
        }

        [HttpPost]
        public string Post([FromBody]AnalysisTemplate form) {
            Status status = new Status();
            //status = EmailService.ProcessEmail(form);

            return status.GetResultMessage();
        }

        // PUT: api/Email/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Email/5
        public void Delete(int id)
        {
        }
    }
}
