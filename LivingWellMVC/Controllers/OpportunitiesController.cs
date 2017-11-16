using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LivingWellMVC.Controllers
{
    public class OpportunitiesController : Controller
    {
        // GET: Opportunities
        public ActionResult Index() {
            return View();
        }
    }
}