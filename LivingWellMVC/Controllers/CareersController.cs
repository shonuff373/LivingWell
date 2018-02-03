using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LivingWellMVC.ViewModels;

namespace LivingWellMVC.Controllers{

    public class CareersController : BaseController
    {
        // GET: Opportunities
        public ActionResult Index() {
            info = new BaseViewModel();

            return View(info);
        }

        public ActionResult Careers() {

             info = new BaseViewModel();

            return View(info);
        }

        public ActionResult TherapistApplication() {

            info = new BaseViewModel();

            return View(info);
        }

        private ActionResult DefaultContact() {
            return View("~/Views/Contact/Index.cshtml");
        }

        public ActionResult Apply(string applicationType) {
            return ApplicationRouter(applicationType);
        }

        public ActionResult ApplicationRouter(string applicationType) {
            switch (applicationType) {
                case "Physical":
                case "Occupational":
                case "Speech":
                    return RedirectToAction("TherapistApplication");
                    //break;
                default:
                    return RedirectToAction("DefaultContact");
                    //break;
            }
        }
    }
}