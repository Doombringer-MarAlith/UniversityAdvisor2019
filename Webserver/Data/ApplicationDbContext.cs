using Microsoft.AspNet.Identity.EntityFramework;
using Models;
using System.Data.Entity;
using Webserver.Data.Configuration;
using Webserver.Models;
using WebScraper;
using Webserver.Data.Models;

namespace Webserver.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<University> Universities { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Programme> Programmes { get; set; }

        public ApplicationDbContext() : base("DefaultConnection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new UniversityConfiguration());
            modelBuilder.Configurations.Add(new FacultyConfiguration());
            modelBuilder.Configurations.Add(new ReviewConfiguration());
            modelBuilder.Configurations.Add(new ProgrammeConfiguration());
        }

        public void Initialize()
        {
            DataFixture.Initialize(this);
            FillDatabase.Initialize(this, new Scraper());
        }
    }
}