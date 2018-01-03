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
        public string Post([FromBody]string value)
        {
            return "There was an error with you submission. Please call Living Well Services for further support.";
        }
    }
}
