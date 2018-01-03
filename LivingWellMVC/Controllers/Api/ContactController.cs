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

        [Route("submit")]
        public void Post([FromBody]ContactSubmissionInfo info)
        {
            Status status = new Status();
            ContactWorkflowService workflow = new ContactWorkflowService();

            status = workflow.ProcessWorkflow(info);

            //return status.GetResultMessage();

        }
    }
}
