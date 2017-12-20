using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using LivingWellMVC.Models;
using LivingWellMVC.WorkflowServices;

namespace LivingWellMVC.Controllers.Api
{    
    [RoutePrefix("api/contact")]
    public class ContactController : ApiController
    {
        // GET: api/Contact
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Contact/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Contact
        [Route("submit")]
        public string Post([FromBody]ContactSubmissionInfo info)
        {
            Status status = new Status();
            ContactWorkflowService workflow = new ContactWorkflowService();

            status = workflow.ProcessWorkflow(info);


            return status.GetResultMessage();

        }

        // PUT: api/Contact/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Contact/5
        public void Delete(int id)
        {
        }
    }
}
