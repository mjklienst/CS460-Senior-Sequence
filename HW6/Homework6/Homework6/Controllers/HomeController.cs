using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homework6.DAL;
using Homework6.Models;
using Homework6.Models.ViewModels;
using System.Diagnostics;


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

        [HttpPost]
        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult About(string itemMatch)
        {
            if (itemMatch == null || itemMatch == "")
            {
                return (RedirectToAction("Index"));
            }

            List<StockItemViewModel> result = db.StockItems.Select(p => new StockItemViewModel
            {
                StockItemName = p.StockItemName,
                Size = p.Size,
                RecommendedRetailPrice = p.RecommendedRetailPrice,
                TypicalWeightPerUnit = p.TypicalWeightPerUnit,
                LeadTimeDays = p.LeadTimeDays,
                ValidFrom = p.ValidFrom,
                CustomFields = p.CustomFields,
                Tags = p.Tags,
                Photo = p.Photo
            }).Where(sn => sn.StockItemName.Contains(itemMatch)).ToList();

            return View(result);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}