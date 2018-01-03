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

        [Route("submit")]
        [HttpPost]
        public void Post([FromBody]AnalysisSubmissionInfo info) {
            Status status = new Status();
            AnalysisWorkflowService workflow = new AnalysisWorkflowService();

            status = workflow.ProcessWorkflow(info);


            //return status.GetResultMessage();
        }

    }
}
