using Homework8.Models;

namespace Homework8.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class hw8Context : DbContext
    {
        //if you want to run locally, change base to "hw8Context", 
        //else Azure publish: "hw8Context_Azure". These names are in Web.config
        public hw8Context() 
            : base("name=hw8Context")
        {
        }

        public virtual DbSet<Athlete> Athletes { get; set; }
        public virtual DbSet<Coach> Coaches { get; set; }
        public virtual DbSet<EventAthleteResult> EventAthleteResults { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Meet> Meets { get; set; }
        public virtual DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Athlete>()
                .HasMany(e => e.EventAthleteResults)
                .WithRequired(e => e.Athlete)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Coach>()
                .HasMany(e => e.Teams)
                .WithRequired(e => e.Coach)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.EventAthleteResults)
                .WithRequired(e => e.Event)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Meet>()
                .HasMany(e => e.EventAthleteResults)
                .WithRequired(e => e.Meet)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Team>()
                .HasMany(e => e.Athletes)
                .WithRequired(e => e.Team)
                .WillCascadeOnDelete(false);
        }
    }
}
