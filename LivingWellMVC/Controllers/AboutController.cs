﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LivingWellMVC.ViewModels;

namespace LivingWellMVC.Controllers {
    public class AboutController : BaseController {
        // GET: About
        public ActionResult Index() {
            info = new BaseViewModel();

            return View(info);
        }

        public ActionResult About() {

            info = new BaseViewModel();

            return View(info);
        }

    }
}
