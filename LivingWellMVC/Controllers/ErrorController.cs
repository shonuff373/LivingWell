using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LivingWellMVC.ViewModels;

namespace LivingWellMVC.Controllers
{
    public class ErrorController : BaseController
    {
        // GET: Error
        public ActionResult Index() {
            return RedirectToAction("Error");
        }

        public ActionResult Error() {

            info = new BaseViewModel();

            return View(info);
        }
    }
}