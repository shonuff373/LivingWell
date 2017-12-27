using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using LivingWellMVC.Models;
using LivingWellMVC.WorkflowServices;

namespace LivingWellMVC.Controllers.Api {

    [RoutePrefix("api/application")]
    public class ApplicationController : ApiController
    {
        // GET: api/Application
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Application/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Application
        [Route("submit")]
        public string Post([FromBody]ApplicationSubmissionInfo info)
        {
            Status status = new Status();
            ApplicationWorkflowService workflow = new ApplicationWorkflowService();

            status = workflow.ProcessWorkflow(info);
            
            return status.GetResultMessage();
        }

        [Route("submit")]
        public string Post(string firstName, string lastName, string name, string phone, string secondaryPhone, string emailAddress,
                         string addressOne, string addressTwo, string city, string state, string postalCode, string positionType,
                         string positionStatus, string weeklyHours, string referral, string resumeFileName, string message) {
            ApplicationSubmissionInfo info = new ApplicationSubmissionInfo(firstName,  lastName,  name,  phone,  secondaryPhone,  emailAddress,
                          addressOne,  addressTwo,  city,  state,  postalCode,  positionType,
                          positionStatus,  weeklyHours,  referral,  resumeFileName,  message);
            Status status = new Status();
            ApplicationWorkflowService workflow = new ApplicationWorkflowService();

            status = workflow.ProcessWorkflow(info);

            return status.GetResultMessage();
        }


        // PUT: api/Application/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Application/5
        public void Delete(int id)
        {
        }
    }
}
