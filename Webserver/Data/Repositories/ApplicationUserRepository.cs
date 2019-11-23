using Microsoft.AspNet.Identity.EntityFramework;
using Webserver.Data.Infrastructure;
using Webserver.Models;

namespace Webserver.Data.Repositories
{
    public class ApplicationUserRepository : UserStore<ApplicationUser>
    {
        public ApplicationUserRepository(IDatabaseFactory dbFactory) : base(dbFactory.Initialize()) { }
    }
}