using System.Linq;
using WebScraper;

namespace Webserver.Data.Infrastructure
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private ApplicationDbContext _dbContext;
        private DatabaseFiller _dbFiller;
        private IGatherDatabase _scraper;

        public DatabaseFactory(DatabaseFiller dbFiller, IGatherDatabase scraper)
        {
            _dbFiller = dbFiller;
            _scraper = scraper;
        }

        public ApplicationDbContext Initialize()
        {
            if (_dbContext == null)
            {
                _dbContext = new ApplicationDbContext();
                _dbContext.Initialize();
            }

            if (!_dbContext.Universities.Any() && !_dbContext.Faculties.Any() && !_dbContext.Programmes.Any())
            {
                _dbFiller.Initialize(_dbContext, _scraper);
            }

            return _dbContext;
        }
    }
}