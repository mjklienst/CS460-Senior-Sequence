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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;

namespace Homework8.Controllers
{
    public class HomeController : Controller
    {

        //Class containing all graph data that we need
        public class graphStuff
        {
            public string MeetDate { get; set; }
            public string RaceTime { get; set; }
            public string Event1 { get; set; }

        }

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

            //Populating AthleteList with query above
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

            //Whole list of everything
            List<AthleteResultViewModel> Results = new List<AthleteResultViewModel>
                {
                    new AthleteResultViewModel{

                    //List for athletes
                    athleteResults = AthleteList
 }
                };
            return View(Results);
        }


        public ActionResult Graph()
        {
            return View();
        }

        public ActionResult GraphMath()
        {
            string athleteName = Request.QueryString["athleteName"];
            string eventType = Request.QueryString["Event"];

            //query to find all of the athletes with certain name and certain event type, returns date, time, and event
            var graphStuff = db.Athletes.Where(a => a.Name.Contains(athleteName))
            .SelectMany(x => x.EventAthleteResults)
            .Select(x => new { x.Meet.MeetDate, x.RaceTime, x.Event.Event1 })
            .Where(x => x.Event1.Contains(eventType))
            .OrderBy(x => x.MeetDate).ToList();

            List<GraphViewModel> graphInfo = new List<GraphViewModel>();


            //Populating graphInfo with query above
            for (int i = 0; i < graphStuff.Count; i++)
            {
                string meetString = graphStuff.ElementAt(i).MeetDate.ToString();

                graphInfo.Add(new GraphViewModel
                {
                    MeetDate = meetString,
                    RaceTime = graphStuff.ElementAt(i).RaceTime,
                    Event1 = graphStuff.ElementAt(i).Event1
                });
            }

            string jsonString = JsonConvert.SerializeObject(graphInfo, Formatting.Indented);
            return new ContentResult
            {
                Content = jsonString,
                ContentType = "application/json",
                ContentEncoding = System.Text.Encoding.UTF8
            };
        }

        public ActionResult TeamInfo()
        {
            //query to find all of the teams
            var teamStuff = db.Teams.Select(x => x.Name).ToList();

            List<DropDownViewModel> dropDownStuff = new List<DropDownViewModel>();

            for (int i = 0; i < teamStuff.Count; i++)
            {
                dropDownStuff.Add(new DropDownViewModel
                {
                    TeamName = teamStuff.ElementAt(i)
                });
            }

            string jsonString = JsonConvert.SerializeObject(dropDownStuff, Formatting.Indented);
            return new ContentResult
            {
                Content = jsonString,
                ContentType = "application/json",
                ContentEncoding = System.Text.Encoding.UTF8
            };
        }

        public ActionResult Athletes()
        {
            string teamName = Request.QueryString["teamName"];

            //query to find all athletes on a certain team
            var athleteStuff = db.Athletes.Where(x => x.Team.Name.Contains(teamName))
                .Select(x => x.Name).ToList();

            List<DropDownViewModel> dropDownStuff = new List<DropDownViewModel>();

            for (int i = 0; i < athleteStuff.Count; i++)
            {
                dropDownStuff.Add(new DropDownViewModel
                {
                    AthleteName = athleteStuff.ElementAt(i)
                });
            }

            string jsonString = JsonConvert.SerializeObject(dropDownStuff, Formatting.Indented);
            return new ContentResult
            {
                Content = jsonString,
                ContentType = "application/json",
                ContentEncoding = System.Text.Encoding.UTF8
            };
        }

        public ActionResult Events()
        {
            string teamName = Request.QueryString["teamName"];
            string athleteName = Request.QueryString["athleteName"];

            //query to find all events w/ certain athletes on certain team
            var eventStuff = db.Athletes.Where(x => x.Team.Name.Contains(teamName))
           .Where(x => x.Name.Contains(athleteName))
           .SelectMany(x => x.EventAthleteResults)
           .Select(x => x.Event.Event1).ToList();

            List<DropDownViewModel> dropDownStuff = new List<DropDownViewModel>();

            for (int i = 0; i < eventStuff.Count; i++)
            {
                dropDownStuff.Add(new DropDownViewModel
                {
                    Event1 = eventStuff.ElementAt(i)
                });
            }

            string jsonString = JsonConvert.SerializeObject(dropDownStuff, Formatting.Indented);
            return new ContentResult
            {
                Content = jsonString,
                ContentType = "application/json",
                ContentEncoding = System.Text.Encoding.UTF8
            };
        }
    }
}