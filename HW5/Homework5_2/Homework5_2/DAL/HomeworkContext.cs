using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Homework5_2.Models;

namespace Homework5_2.DAL
{
    public class HomeworkContext : DbContext
    {
        public HomeworkContext() : base("name=Homeworks")
        {
            Database.SetInitializer<HomeworkContext>(null);
        }
        public virtual DbSet<Homework> Homeworks { get; set; }
    }
}