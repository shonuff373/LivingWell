using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LivingWellMVC.ViewModels;

namespace LivingWellMVC.Controllers {
    public class PartnerController : BaseController {
        // GET: Partner
        public ActionResult Index() {
            info = new BaseViewModel();

            return View(info);
        }
    }
}