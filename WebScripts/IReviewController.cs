using Models.Models;

namespace WebScripts
{
    public interface IReviewController
    {
        string Get(string guid, int guidType);
        string Get(string guid);
        void Post(Review review);
    }
}