﻿using Microsoft.AspNet.Identity.EntityFramework;
using Models;
using System.Data.Entity;
using Webserver.Data.Configuration;
using Webserver.Data.Models;
using Webserver.Models;

namespace Webserver.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<University> Universities { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public ApplicationDbContext() : base("DefaultConnection")
        {
            DataFixture.Initialize(this);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UniversityConfiguration());
            modelBuilder.Configurations.Add(new FacultyConfiguration());
            modelBuilder.Configurations.Add(new ReviewConfiguration());
        }
    }
}