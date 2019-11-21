using Models;
using Webserver.Data.Infrastructure;

namespace Webserver.Data.Repositories
{
    public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
    {
        public ReviewRepository(IDatabaseFactory dbFactory) : base(dbFactory) { }
    }
}