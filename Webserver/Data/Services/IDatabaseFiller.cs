using System.Threading.Tasks;

namespace Webserver.Data.Services
{
    public interface IDatabaseFiller
    {
        Task Fill(ApplicationDbContext dbContext);
    }
}