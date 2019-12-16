using Microsoft.AspNet.Identity.EntityFramework;
using Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Webserver.Data.Configuration;
using Webserver.Data.Models;
using Webserver.Data.Services;
using Webserver.Models;

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

        public async Task Initialize(IDatabaseFiller dbFiller)
        {
            if (!Universities.Any() && !Faculties.Any() && !Programmes.Any())
            {
                DataFixture.Initialize(this);
                await dbFiller.Fill(this);
            }
        }
    }
}