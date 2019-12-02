using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using Homework8.DAL;

namespace Homework8.Models.ViewModels
{
    public class DropDownViewModel
    {
        //For team table, getting team name
        public string TeamName { get; set; }

        //For athlete table, getting athlete name
        public string AthleteName { get; set; }

        //For event table, getting race event/distance
        public string Event1 { get; set; }
    }
}