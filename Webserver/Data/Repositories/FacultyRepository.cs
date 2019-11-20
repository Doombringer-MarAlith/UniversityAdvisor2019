using Webserver.Data.Infrastructure;
using Webserver.Models;

namespace Webserver.Data.Repositories
{
    public class FacultyRepository : RepositoryBase<Faculty>, IFacultyRepository
    {
        public FacultyRepository(IDatabaseFactory dbFactory) : base(dbFactory) { }
    }
}