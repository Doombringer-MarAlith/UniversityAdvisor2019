using App;
using ServerCallFromApp;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Objektinis.FormManagers
{
    public class LoginFormManager : BaseFormManager, ILoginFormManager
    {
        public LoginFormManager(IDataManipulations dataManipulations, FormManagerData formManagerData) : base(dataManipulations, formManagerData)
        {
        }

        public async Task TryToLogIn(string email, string password, Form loginForm)
        {
            var result = await DataManipulations.GetDataFromServer($"account/login/{email}/{password}");
            if (String.IsNullOrEmpty(result))
            {
                MessageBox.Show("Account with such credentials does not exist!");
            }
            else
            {
                FormManagerData.CurrentUserGuid = result;
                ChangeForm(loginForm, GetForm(FormType.FormUniversities));
            }
        }
    }
}