using System.Threading.Tasks;

namespace App.FormManagers
{
    public interface ISignUpFormManager : IBaseFormManager
    {
        Task<int> CreateUser(string username, string email, string password);

        bool IsEmailValid(string email);
    }
}