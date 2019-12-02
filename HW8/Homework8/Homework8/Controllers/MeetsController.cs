using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Homework8.DAL;
using Homework8.Models;

namespace Homework8.Controllers
{
    public class MeetsController : Controller
    {
        private hw8Context db = new hw8Context();

        // GET: Meets
        public ActionResult Index()
        {
            return View(db.Meets.ToList());
        }

        // GET: Meets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Meets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MeetID,Location,MeetDate")] Meet meet)
        {
            if (ModelState.IsValid)
            {
                db.Meets.Add(meet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(meet);
        }

    }
}
