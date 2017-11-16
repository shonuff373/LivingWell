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
        public string Post([FromBody]string firstName, 
            string lastName, string communityName, string addressOne,
            string addressTwo, string city, string state, string postalCode, string phone, string email)
        {
            Status status = new Status();
            AnalysisSubmissionInfo info = new AnalysisSubmissionInfo(firstName, lastName, communityName, addressOne, addressTwo, city, state, postalCode, phone, email);
            AnalysisWorkflowService workflow = new AnalysisWorkflowService();

            status = workflow.ProcessWorkflow(info);


            return status.GetResultMessage();
        }

        [HttpPost]
        public string Post([FromBody]AnalysisSubmissionInfo info) {
            Status status = new Status();
            AnalysisWorkflowService workflow = new AnalysisWorkflowService();

            status = workflow.ProcessWorkflow(info);
            

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
