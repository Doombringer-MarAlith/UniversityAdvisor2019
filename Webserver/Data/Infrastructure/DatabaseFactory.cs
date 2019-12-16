using System.Threading.Tasks;
using Webserver.Data.Services;

namespace Webserver.Data.Infrastructure
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private readonly IDatabaseFiller _dbFiller;
        private ApplicationDbContext _dbContext;

        public DatabaseFactory(IDatabaseFiller dbFiller)
        {
            _dbFiller = dbFiller;
        }

        public ApplicationDbContext Initialize()
        {
            if (_dbContext == null)
            {
                _dbContext = new ApplicationDbContext();
                Task task = Task.Run(async () => await _dbContext.Initialize(_dbFiller));
                task.Wait();
            }

            return _dbContext;
        }
    }
}