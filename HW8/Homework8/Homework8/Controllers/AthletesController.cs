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
    public class AthletesController : Controller
    {
        private hw8Context db = new hw8Context();

        // GET: Athletes
        public ActionResult Index()
        {
            var athletes = db.Athletes.Include(a => a.Team);
            return View(athletes.ToList());
        }

        // GET: Athletes/Create
        public ActionResult Create()
        {
            ViewBag.TeamID = new SelectList(db.Teams, "TeamID", "Name");
            return View();
        }

        // POST: Athletes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AthleteID,Name,Gender,TeamID")] Athlete athlete)
        {
            if (ModelState.IsValid)
            {
                db.Athletes.Add(athlete);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TeamID = new SelectList(db.Teams, "TeamID", "Name", athlete.TeamID);
            return View(athlete);
        }

      
    }
}
