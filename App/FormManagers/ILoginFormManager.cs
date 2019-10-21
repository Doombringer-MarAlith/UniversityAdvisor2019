using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public interface ILoginFormManager
    {
        Task TryToLogIn(string email, string password, Form loginForm);
    }
}