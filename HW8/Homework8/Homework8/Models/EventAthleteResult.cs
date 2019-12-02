namespace Homework8.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EventAthleteResult
    {
        [Key]
        public int EARID { get; set; }

        public float RaceTime { get; set; }

        public int EventID { get; set; }

        public int AthleteID { get; set; }

        public int MeetID { get; set; }

        public virtual Athlete Athlete { get; set; }

        public virtual Event Event { get; set; }

        public virtual Meet Meet { get; set; }
    }
}
