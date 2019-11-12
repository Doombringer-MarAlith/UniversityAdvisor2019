using System.Threading.Tasks;
using Models.Models;

namespace WebScripts
{
    public interface IUniversityController
    {
        Task<string> Get(string name);
        void Post(University university);
    }
}