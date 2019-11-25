using Models;
using Webserver.Data.Infrastructure;

namespace Webserver.Data.Repositories
{
    public class FacultyRepository : RepositoryBase<Faculty>, IFacultyRepository
    {
        public FacultyRepository(IDatabaseFactory dbFactory) : base(dbFactory)
        {
        }
    }
}