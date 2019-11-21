using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
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
    }
}