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
                _dbContext.Initialize(_dbFiller);
            }

            return _dbContext;
        }
    }
}