using Models;
using Webserver.Data.Infrastructure;

namespace Webserver.Data.Repositories
{
    public class ProgrammeRepository : RepositoryBase<Programme>, IProgrammeRepository
    {
        public ProgrammeRepository(IDatabaseFactory dbFactory) : base(dbFactory)
        {
        }
    }
}