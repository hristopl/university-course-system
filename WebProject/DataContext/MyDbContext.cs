using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebProject.Entities;

namespace WebProject.DataContext
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("WebProjectDB")
        {
            Database.SetInitializer<MyDbContext>(new DropCreateDatabaseIfModelChanges<MyDbContext>());
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}