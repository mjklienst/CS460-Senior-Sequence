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
    public class EventAthleteResultsController : Controller
    {
        private hw8Context db = new hw8Context();

        // GET: EventAthleteResults
        public ActionResult Index()
        {
            var eventAthleteResults = db.EventAthleteResults.Include(e => e.Athlete).Include(e => e.Event).Include(e => e.Meet);
            return View(eventAthleteResults.ToList());
        }


        // GET: EventAthleteResults/Create
        public ActionResult Create()
        {
            ViewBag.AthleteID = new SelectList(db.Athletes, "AthleteID", "Name");
            ViewBag.EventID = new SelectList(db.Events, "EventID", "Event1");
            ViewBag.MeetID = new SelectList(db.Meets, "MeetID", "Location");
            return View();
        }

        // POST: EventAthleteResults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EARID,RaceTime,EventID,AthleteID,MeetID")] EventAthleteResult eventAthleteResult)
        {
            if (ModelState.IsValid)
            {
                db.EventAthleteResults.Add(eventAthleteResult);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AthleteID = new SelectList(db.Athletes, "AthleteID", "Name", eventAthleteResult.AthleteID);
            ViewBag.EventID = new SelectList(db.Events, "EventID", "Event1", eventAthleteResult.EventID);
            ViewBag.MeetID = new SelectList(db.Meets, "MeetID", "Location", eventAthleteResult.MeetID);
            return View(eventAthleteResult);
        }

    }
}
