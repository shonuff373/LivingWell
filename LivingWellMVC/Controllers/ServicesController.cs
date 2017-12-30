﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LivingWellMVC.ViewModels;

namespace LivingWellMVC.Controllers {
    public class ServicesController : BaseController {

        public ActionResult Index() {
            return RedirectToAction("Services");
        }

        public ActionResult Services() {

            info = new BaseViewModel();

            return View(info);
        }
    }
}