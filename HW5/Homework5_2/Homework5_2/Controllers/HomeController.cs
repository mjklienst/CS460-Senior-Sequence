using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Homework5_2.DAL;
using Homework5_2.Models;

namespace Homework5_2.Controllers
{
    public class HomeController : Controller
    {
        private HomeworkContext db = new HomeworkContext();

        private IList<SelectListItem> PriorityList = new List<SelectListItem>
        {
            new SelectListItem { Value = "High", Text = "High" },
            new SelectListItem { Value = "Medium", Text = "Medium" },
            new SelectListItem { Value = "Low", Text = "Low" }
        };

        // GET: Homework
        public ActionResult Index()
        {
            return View(db.Homeworks.OrderByDescending(p => p.DueDate).ThenByDescending(p => p.DueTime).ToList());
        }

        public ActionResult Create()
        {
            ViewBag.PriorityList = PriorityList;
            return View();
        }

        // POST: Homework/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,HomeworkTitle,Notes,DueDate,DueTime,Department,CourseNum,Priority")] Homework homework)
        {
            if (ModelState.IsValid)
            {
                db.Homeworks.Add(homework);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(homework);
        }

    }
}