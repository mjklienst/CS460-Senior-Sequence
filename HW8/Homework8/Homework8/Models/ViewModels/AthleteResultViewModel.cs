using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework8.Models.ViewModels
{
    public class AthleteResultList
    {
        //For the AthleteResultList
        public DateTime MeetDate { get; set; }
        public string Event1 { get; set; }

        public string Location { get; set; }

        public float RaceTime { get; set; }

    }
    public class AthleteResultViewModel
    {
        //For the AthleteResultList
        public List<AthleteResultList> athleteResults { get; set; }
    }
}