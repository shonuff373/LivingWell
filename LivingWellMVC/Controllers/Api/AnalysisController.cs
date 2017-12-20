using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using LivingWellMVC.Models;
using LivingWellMVC.WorkflowServices;

namespace LivingWellMVC.Controllers.Api {
    [RoutePrefix("api/analysis")]
    public class AnalysisController : ApiController {
        // GET: api/Email
        public IEnumerable<string> Get() {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Email/5
        public string Get(int id) {
            return "value";
        }

        // POST: api/Contact
        [Route("submit")]
        [HttpPost]
        public string Post([FromBody]AnalysisSubmissionInfo info) {
            Status status = new Status();
            AnalysisWorkflowService workflow = new AnalysisWorkflowService();

            status = workflow.ProcessWorkflow(info);


            return status.GetResultMessage();
        }

        // PUT: api/Email/5
        public void Put(int id, [FromBody]string value) {
        }

        // DELETE: api/Email/5
        public void Delete(int id) {
        }
    }
}
