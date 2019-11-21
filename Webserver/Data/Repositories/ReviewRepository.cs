using Webserver.Data.Infrastructure;
using Webserver.Models;

namespace Webserver.Data.Repositories
{
    public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
    {
        public ReviewRepository(IDatabaseFactory dbFactory) : base(dbFactory) { }
    }
}