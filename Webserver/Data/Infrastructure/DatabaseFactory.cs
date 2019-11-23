namespace Webserver.Data.Infrastructure
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private ApplicationDbContext _dbContext;

        public ApplicationDbContext Initialize()
        {
            return _dbContext ?? (_dbContext = new ApplicationDbContext());
        }
    }
}