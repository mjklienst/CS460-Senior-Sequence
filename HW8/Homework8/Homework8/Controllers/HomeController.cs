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
using Homework8.Models.ViewModels;

namespace Homework8.Controllers
{
    public class HomeController : Controller
    {
        private hw8Context db = new hw8Context();
        public ActionResult StudentResult(string searchName)
        {
            //Athlete name entered by coach
            ViewBag.searchResult = searchName;

            //Linq query for finding the date, event, location, and racetime for certain athlete
            var athleteResults = db.Athletes.Where(a => a.Name.Contains(searchName))
            .SelectMany(x => x.EventAthleteResults)
            .Select(x => new { x.Meet.MeetDate, x.Event.Event1, x.Meet.Location, x.RaceTime })
            .OrderBy(x => x.Event1)
            .ThenBy(x => x.MeetDate).ToList();
            List<AthleteResultList> AthleteList = new List<AthleteResultList>();

            //Populating AthleteList with queries above
            for (int i = 0; i < athleteResults.Count; i++)
            {
                AthleteList.Add(new AthleteResultList
                {
                    MeetDate = athleteResults.ElementAt(i).MeetDate,
                    Event1 = athleteResults.ElementAt(i).Event1,
                    Location = athleteResults.ElementAt(i).Location,
                    RaceTime = athleteResults.ElementAt(i).RaceTime

                });
            }
            System.Diagnostics.Debug.WriteLine("the top purchasers 1 : " + AthleteList);


            for (int i = 0; i < athleteResults.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine(athleteResults[i]);
            }
            for (int i = 0; i < AthleteList.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine("the top purchasers 2 : " + AthleteList[i].MeetDate);
            }
            System.Diagnostics.Debug.WriteLine(athleteResults);

            //Whole list of everything
            List<AthleteResultViewModel> Results = new List<AthleteResultViewModel>
                {
                    new AthleteResultViewModel{

                    //List for top purchasers
                    athleteResults = AthleteList
 }
                };
            for (int i = 0; i < athleteResults.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine("the top purchasers 3 : " + athleteResults[i].Event1);
            }
            return View(Results);
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