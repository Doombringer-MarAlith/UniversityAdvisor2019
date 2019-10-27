using Models.Models;
using Newtonsoft.Json;
using ServerCallFromApp;
using System;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace App.FormManagers
{
    public class SignUpFormManager : BaseFormManager, ISignUpFormManager
    {
        public SignUpFormManager(IDataManipulations dataManipulations, FormManagerData formManagerData) : base(dataManipulations, formManagerData)
        {
        }

        public async Task<int> CreateUser(string username, string email, string password)
        {
            // Check for existing email
            var data = await DataManipulations.GetDataFromServer($"account/checkByEmail/{email}/{true}");
            if (!String.IsNullOrEmpty(data))
            {
                return (int)CreateUserReturn.EMAIL_TAKEN;
            }

            // Check for existing username
            data = await DataManipulations.GetDataFromServer($"account/checkByUsername/{username}/{0}");
            if (!String.IsNullOrEmpty(data))
            {
                return (int)CreateUserReturn.USERNAME_TAKEN;
            }

            // Check for existing guid
            Account account = new Account()
            {
                Name = username,
                Email = email,
                Password = password
            };

            do
            {
                account.Guid = Guid.NewGuid().ToString();
                data = await DataManipulations.GetDataFromServer($"account/{account.Guid}");
            }
            while (!String.IsNullOrEmpty(data));

            // Create an account
            await DataManipulations.PostDataToServer("account/create", JsonConvert.SerializeObject(account));
            return (int)CreateUserReturn.SUCCESS;
        }

        public bool IsEmailValid(string email)
        {
            Regex regex = new Regex("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);

            MatchCollection matches = regex.Matches(email);
            return matches.Count > 0;
        }
    }
}