using AcceptanceTests.TestHelpers;
using Models.Models;
using Newtonsoft.Json;
using ServerCallFromApp;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace AcceptanceTests
{
    public class ServerSide_Should
    {
        [Theory]
        [AutoMoqData]
        public async Task Create_Login_AndGetAccount(Account acc)
        {
            DataManipulations dataManipulations = new DataManipulations(new HttpClient());
            //string guid = Guid.NewGuid().ToString();
            var account = acc;/* new Account()
            {
                Name = Helper.GenerateRandomString(50),
                Password = Helper.GenerateRandomString(50),
                Email = Helper.GenerateRandomString(50),
                Guid = guid
            };
            */
            await dataManipulations.PostDataToServer("account/create", JsonConvert.SerializeObject(account));
            var returnGuid = await dataManipulations.GetDataFromServer($"account/login/{account.Email}/{account.Password}");
            Assert.NotNull(returnGuid);
            Assert.Equal(acc.Guid, returnGuid);

            var fetchedAccount = JsonConvert.DeserializeObject<Account>(await dataManipulations.GetDataFromServer($"account/{acc.Guid}"));
            Assert.Equal(account.Guid, fetchedAccount.Guid);
            Assert.Equal(account.Name, fetchedAccount.Name);
            Assert.Equal(account.Password, fetchedAccount.Password);
            Assert.Equal(account.Email, fetchedAccount.Email);
        }
    }
}