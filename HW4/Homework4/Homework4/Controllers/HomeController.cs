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

        [HttpPost]
        public ActionResult RGB_Color(int R, int G, int B)
        {
            Color color = Color.FromArgb(255, R, G, B); //Setting alpha to 0, rest to R,G,B values
            string hex = color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2"); //Converting to hex
            if (ModelState.IsValid)
            {
                ViewBag.Hex = "#" + hex;
                ViewBag.Color = "width: 150px; height: 150px; border: 1px solid; background-color: #" + hex + "; ";
                return View();
            }
            else
            {
                return View();
            }
        }

        public ActionResult Color_Interpolator()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}