using AcceptanceTests.TestHelpers;
using Models.Models;
using Newtonsoft.Json;
using ServerCallFromApp;
using System;
using Xunit;

namespace AcceptanceTests
{
    public class ServerSide_Should
    {
        [Fact]
        public void Create_Login_AndGetAccount()
        {
            string guid = Guid.NewGuid().ToString();
            var account = new Account()
            {
                Name = Helper.GenerateRandomString(50),
                Password = Helper.GenerateRandomString(50),
                Email = Helper.GenerateRandomString(50),
                Guid = guid
            };

            DataManipulations.PostDataToServer("account/create", JsonConvert.SerializeObject(account));
            var returnGuid = DataManipulations.GetDataFromServer($"account/login/{account.Email}/{account.Password}");
            Assert.NotNull(returnGuid);
            Assert.Equal(guid, returnGuid);

            var fetchedAccount = JsonConvert.DeserializeObject<Account>(DataManipulations.GetDataFromServer($"account/{guid}"));
            Assert.Equal(fetchedAccount.Guid, fetchedAccount.Guid);
            Assert.Equal(fetchedAccount.Name, fetchedAccount.Name);
            Assert.Equal(fetchedAccount.Password, fetchedAccount.Password);
            Assert.Equal(fetchedAccount.Age, fetchedAccount.Age);
            Assert.Equal(fetchedAccount.Email, fetchedAccount.Email);
        }
    }
}
