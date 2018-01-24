using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Administration;
using System.Web.Http;
using Newtonsoft.Json;


namespace LivingWellMVC.Controllers.Api
{
    [RoutePrefix("api/uploads")]
    public class UploadsController : ApiController
    {
        [HttpPost]
        [Route("resume")]
        public void Upload() {
            LivingWellMVC.Models.CompanyInfo company = new Models.CompanyInfo();
            //http://ajeeshms.in/articles/upload-files-using-ajax-in-asp-net-mvc/
            for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++) {
                HttpPostedFileBase file = new System.Web.HttpPostedFileWrapper(HttpContext.Current.Request.Files[i]); //Uploaded file
                //Use the following properties to get file's name, size and MIMEType
                int fileSize = file.ContentLength;
                string fileName = file.FileName;
                string mimeType = file.ContentType;
                System.IO.Stream fileContent = file.InputStream;
                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data/Uploads"), fileName);
                //Delete existing resume file. "*" matches all characters
                //System.IO.File.Delete("App_Data/Uploads/");
                //To save file, use SaveAs method
                
                //file.SaveAs(HttpContext.Current.Server.MapPath("~/App_Data/Uploads")); //File will be saved in application root
                file.SaveAs(path);
            }

        }
    }
}