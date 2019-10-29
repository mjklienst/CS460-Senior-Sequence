using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;

namespace Homework4.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RGB_Color()
        {
            return View();
        }

        //Not using below page
        public ActionResult Color_Interpolator()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}