using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LivingWellMVC.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        public ActionResult Index()
        {
            LivingWellMVC.Models.LivingWellInfo info = new Models.LivingWellInfo();
            return View(info);
        }

    }
}
