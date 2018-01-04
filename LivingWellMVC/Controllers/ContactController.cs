using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LivingWellMVC.ViewModels;

namespace LivingWellMVC.Controllers {

    public class ContactController : BaseController {
        // GET: Contact
        public ActionResult Index() {
            info = new BaseViewModel();

            return View(info);
        }

        public ActionResult Contact() {

            info = new BaseViewModel();

            return View(info);
        }

    }
}
