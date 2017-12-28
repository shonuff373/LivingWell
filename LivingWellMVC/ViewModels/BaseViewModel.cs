using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LivingWellMVC.Models;

namespace LivingWellMVC.ViewModels {
    public class BaseViewModel {
        public LivingWellInfo LivingWellInfo { get; set; }

        public BaseViewModel() {
            this.LivingWellInfo = new LivingWellInfo();
        }
    }
}