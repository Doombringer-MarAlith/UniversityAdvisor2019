using Models.Models;

namespace WebScripts
{
    public interface IAccountController
    {
        string GetAccount(string id);
        string GetGuidByEmail(string email);
        string GetBuidByUserName(string username);
        string GetGuidByEmailAndPassword(string email, string password);
        void PostAccount(Account account);
    }
}