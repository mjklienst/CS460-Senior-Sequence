using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homework6.DAL;
using Homework6.Models;
using Homework6.Models.ViewModels;
using System.Diagnostics;
using System.Data.Entity;



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

            //Feature 1, creating first table
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

            //Table for supplier, starting feature 2 branch
            var SupplierInformation = db.StockItems
                        .Where(p => p.StockItemName == itemMatch)
                        .Select(p => p.Supplier).ToList();

            //Info for the table
            var topCustomers = db.StockItems.Where(person => person.StockItemName.Contains(itemMatch))
                .Select(x => x.Person)
                            .SelectMany(x => x.Customers1)
                            .GroupBy(x => x.CustomerID)
                            .OrderByDescending(x => x.SelectMany(p => p.Orders).SelectMany(y => y.OrderLines).Sum(l => l.Quantity))
                            .Take(10)
                            .Select(x => x.FirstOrDefault().CustomerName)
                            .ToList();

            //Info for the table
            var topQuantity = db.StockItems.Where(person => person.StockItemName.Contains(itemMatch))
                .Select(x => x.Person)
                            .SelectMany(x => x.Customers1)
                            .GroupBy(x => x.CustomerID)
                            .OrderByDescending(x => x.SelectMany(p => p.Orders).SelectMany(y => y.OrderLines).Sum(l => l.Quantity))
                            .Take(10)
                            .Select(x => x.SelectMany(p => p.Orders).SelectMany(y => y.OrderLines).Sum(l => l.Quantity))
                            .ToList();

            //Creating new PurchaseList
            List<PurchaseList> TopPurchasers = new List<PurchaseList>();

            //Populating PurchaseList with queries above
            for (int i = 0; i < 10; i++)
            {
                TopPurchasers.Add(new PurchaseList
                {
                    CustomerName = topCustomers.ElementAt(i),
                    Quantity = topQuantity.ElementAt(i)
                });
            }

            //Whole list of everything
            List<StockItemViewModel> Customers = new List<StockItemViewModel>
                {
                    new StockItemViewModel{
                    //StockItem details
                    StockItemName = result.First().StockItemName,
                    Size = result.First().Size,
                    RecommendedRetailPrice = result.First().RecommendedRetailPrice,
                    TypicalWeightPerUnit = result.First().TypicalWeightPerUnit,
                    LeadTimeDays = result.First().LeadTimeDays,
                    ValidFrom = result.First().ValidFrom,
                    CustomFields = result.First().CustomFields,
                    Tags = result.First().Tags,

                    //Supplier details
                    SupplierName = SupplierInformation.First().SupplierName,
                    PhoneNumber = SupplierInformation.First().PhoneNumber,
                    FaxNumber = SupplierInformation.First().FaxNumber,
                    WebsiteURL = SupplierInformation.First().WebsiteURL,
                    SupplierID = SupplierInformation.First().SupplierID,
                    FullName = SupplierInformation.Where(x=>x.SupplierID == SupplierInformation.First().SupplierID)
                                    .Select(x => x.Person2)
                                    .Select(x => x.FullName).First(),
                    
                    //Sales history information              
                    Orders = db.StockItems.Where(y => y.StockItemName.Contains(itemMatch))
                                   .SelectMany(x => x.InvoiceLines)
                                   .Count(),

                    GrossSales = db.StockItems.Where(y => y.StockItemName.Contains(itemMatch))
                                   .SelectMany(x => x.InvoiceLines)
                                   .Sum(x => x.ExtendedPrice),

                    GrossProfit = db.StockItems.Where(y => y.StockItemName.Contains(itemMatch))
                                   .SelectMany(x => x.InvoiceLines)
                                   .Sum(x => x.LineProfit),

                    //List for top purchasers
                    ListOfPurchases = TopPurchasers
 }
                };

            return View(Customers);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}