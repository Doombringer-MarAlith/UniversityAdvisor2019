using Models.Models;
using Newtonsoft.Json;
using Objektinis;
using ServerCallFromApp;
using System;
using System.Threading.Tasks;

namespace App
{
    public class SignUpFormManager : BaseFormManager, ISignUpFormManager
    {
        protected SignUpFormManager(IDataManipulations dataManipulations, FormManagerData formManagerData) : base(dataManipulations, formManagerData)
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
            while (String.IsNullOrEmpty(data));

            // Create an account
            await DataManipulations.PostDataToServer("account/create", JsonConvert.SerializeObject(account));
            return (int)CreateUserReturn.SUCCESS;
        }
    }
}