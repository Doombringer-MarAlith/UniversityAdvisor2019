using Webserver.Data.Models;

namespace Webserver.Data.Infrastructure
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private ApplicationDbContext _dbContext;

        public ApplicationDbContext Initialize()
        {
            if (_dbContext == null)
            {
                _dbContext = new ApplicationDbContext();
            }

            DataFixture.Initialize(_dbContext);
            return _dbContext;
        }
    }
}