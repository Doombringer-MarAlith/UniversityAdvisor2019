using System.Threading.Tasks;
using System.Windows.Forms;

namespace App.FormManagers
{
    public interface ILoginFormManager : IBaseFormManager
    {
        Task TryToLogIn(string email, string password, Form loginForm);
    }
}