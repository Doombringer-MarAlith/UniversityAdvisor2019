using System.Threading.Tasks;

namespace App
{
    public interface ISignUpFormManager
    {
        Task<int> CreateUser(string username, string email, string password);
    }
}