using ServerCallFromApp;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public class LoginFormManager : BaseFormManager
    {
        private readonly IDataManipulations _dataManipulations;
        private string _currentUserGuid;

        public LoginFormManager(IDataManipulations dataManipulations) : base(dataManipulations)
        {
            _dataManipulations = dataManipulations;
        }

        internal async Task TryToLogIn(string email, string password, Form loginForm)
        {
            var result = await _dataManipulations.GetDataFromServer($"account/login/{email}/{password}");
            if (String.IsNullOrEmpty(result))
            {
                MessageBox.Show("Account with such credentials does not exist!");
            }
            else
            {
                _currentUserGuid = result;
                ChangeForm(loginForm, GetForm(FormType.FORM_UNIVERSITIES));
            }
        }
    }
}