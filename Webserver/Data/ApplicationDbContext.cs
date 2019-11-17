using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Webserver.Data.Models;
using Webserver.Models;

namespace Webserver.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<University> Universities { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Faculty> Faculties { get; set; }

        public ApplicationDbContext()
            : base(@"Server = (localdb)\madder; Database=UniAdv;Trusted_Connection=True;")
        {
            DataFixture.Initialize(this);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}