using Models;
using Webserver.Data.Infrastructure;

namespace Webserver.Data.Repositories
{
    public class UniversityRepository : RepositoryBase<University>, IUniversityRepository
    {
        public UniversityRepository(IDatabaseFactory dbFactory) : base(dbFactory) { }
    }
}