using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Kindergarten3.Models;

namespace Kindergarten3.Context
{
    public class KgContext : DbContext
    {
        public KgContext() : base("KgContext")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<KgContext>());
        }

        public DbSet<Science> Sciences { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<LessonTableItem> LessonTableItems { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<ActivateTable> ActivateTables { get; set; }



    }
}