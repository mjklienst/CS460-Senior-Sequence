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

        [Required, Display(Name = "Homework Title: ")]
        [StringLength(64)]
        public string HomeworkTitle { get; set; }

        [Required, Display(Name = "Notes: ")]
        [StringLength(128)]
        public string Notes { get; set; }

        [Required, Display(Name = "Due Date: ")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Column(TypeName = "date")]
        public DateTime DueDate { get; set; }

        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        [Required, Display(Name = "Due Time: ")]
        public TimeSpan DueTime { get; set; }

        [Required, Display(Name = "Department: ")]
        [StringLength(3)]
        public string Department { get; set; }

        [Required, Display(Name = "Course Number: ")]
        public int CourseNum { get; set; }

        [Required, Display(Name = "Priority: ")]
        public IEnumerable<System.Web.Mvc.SelectListItem> Priority { get; set; }
    }
}
