using Microsoft.AspNet.Identity.EntityFramework;
using Webserver.Models;

namespace Webserver.Data.Repositories
{
    public class ApplicationUserRepository : UserStore<ApplicationUser>
    {
        public ApplicationUserRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}