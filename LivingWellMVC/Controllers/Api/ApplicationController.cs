using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Administration;
using System.Web.Http;
using Newtonsoft.Json;

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
        [HttpPost]
        public string Post([FromBody]ApplicationSubmissionInfo info)
        {
            Status status = new Status();
            ApplicationWorkflowService workflow = new ApplicationWorkflowService();

            status = workflow.ProcessWorkflow(info);
            
            return status.GetResultMessage();
        }

        [HttpPost]
        [Route("upload")]
        public void Upload(){
            //http://ajeeshms.in/articles/upload-files-using-ajax-in-asp-net-mvc/
            for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++) {
                HttpPostedFileBase file = new System.Web.HttpPostedFileWrapper(HttpContext.Current.Request.Files[i]); //Uploaded file
                //Use the following properties to get file's name, size and MIMEType
                int fileSize = file.ContentLength;
                string fileName = file.FileName;
                string mimeType = file.ContentType;
                System.IO.Stream fileContent = file.InputStream;
                //Delete existing resume file. "*" matches all characters
                System.IO.File.Delete("/Uploads/Resume_*");
                //To save file, use SaveAs method
                file.SaveAs(HttpContext.Current.Server.MapPath("/Uploads/Resume_") + fileName); //File will be saved in application root
            }

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
