using System.Threading.Tasks;
using Objektinis.FormManagers;

namespace App
{
    public interface ISignUpFormManager : IBaseFormManager
    {
        Task<int> CreateUser(string username, string email, string password);
    }
}