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

        [Required(ErrorMessage = "Enter Due Date"), Display(Name = "Due Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        //[Column(TypeName = "date")]
        public DateTime DueDate { get; set; }


        [Required(ErrorMessage = "Enter Due Time"), Display(Name = "Due Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        // [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        public TimeSpan DueTime { get; set; }

        [Required, Display(Name = "Department: ")]
        [StringLength(3)]
        public string Department { get; set; }

        [Required, Display(Name = "Course Number: ")]
        public int CourseNum { get; set; }

        [Required, Display(Name = "Priority: ")]
        public string Priority { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> PriorityList { get; set; }

       
    }
}
