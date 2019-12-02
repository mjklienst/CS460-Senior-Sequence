using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using Homework8.DAL;


namespace Homework8.Models.ViewModels
{
    public class GraphViewModel
    {
        //For event table, getting race event/distance
        public string Event1 { get; set; }

        public string MeetDate { get; set; }
        public float RaceTime { get; set; }


    }
}