namespace Homework8.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Athlete
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Athlete()
        {
            EventAthleteResults = new HashSet<EventAthleteResult>();
        }

        public int AthleteID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression("^Girls$|^Boys$", ErrorMessage = "Please enter either 'Girls' or 'Boys' (case sensitive)")]


        public string Gender { get; set; }

        public int TeamID { get; set; }

        public virtual Team Team { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventAthleteResult> EventAthleteResults { get; set; }
    }
}
