using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homework6.DAL;
using Homework6.Models;
using Homework6.Models.ViewModels;


namespace Homework6.Controllers
{
    public class HomeController : Controller
    {

        WWIContext db = new WWIContext();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string searchString)
        {
            ViewBag.searchResult = searchString;

            List<StockItemViewModel> stockList = db.StockItems.Select(p => new StockItemViewModel
            {
                StockItemName = p.StockItemName,
            }).Where(sn => sn.StockItemName.Contains(searchString)).ToList();

            if (stockList.Count > 0)
            {
                ViewBag.toggle = 1;
                ViewBag.searchResult = "Stock items matching your search: \"" + searchString + "\"";
            }
            else
            {
                ViewBag.searchResult = "I'm sorry, your search returned no results";
                return View(stockList);
            }

            return View(stockList);

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}