namespace Homework5_2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Homeworks")]
    public partial class Homework
    {
        public int ID { get; set; }

        [Required]
        [StringLength(64)]
        public string HomeworkTitle { get; set; }

        [Required]
        [StringLength(128)]
        public string Notes { get; set; }

        [Column(TypeName = "date")]
        public DateTime DueDate { get; set; }

        public TimeSpan DueTime { get; set; }

        [Required]
        [StringLength(3)]
        public string Department { get; set; }

        public int CourseNum { get; set; }

 

        [Required]
        //public string Priority { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> Priority { get; set; }
    }
}
