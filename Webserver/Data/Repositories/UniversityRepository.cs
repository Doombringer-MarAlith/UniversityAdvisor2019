using Webserver.Data.Infrastructure;
using Webserver.Models;

namespace Webserver.Data.Repositories
{
    public class UniversityRepository : RepositoryBase<University>, IUniversityRepository
    {
        public UniversityRepository(IDatabaseFactory dbFactory) : base(dbFactory) { }
    }
}