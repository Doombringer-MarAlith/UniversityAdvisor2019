using System;
using AcceptanceTests.TestHelpers;
using Models.Models;
using Newtonsoft.Json;
using ServerCallFromApp;
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
                Email = "hahaha@gmail.com",
                Guid = guid
            };
            DataManipulations.PostDataToServer("account/create", JsonConvert.SerializeObject(account));
            var returnGuid = DataManipulations.GetDataFromServer($"account/login/{account.Name}/{account.Password}");
            Assert.NotNull(returnGuid);
            Assert.Equal(guid,returnGuid);
            var acc = JsonConvert.DeserializeObject<Account>(DataManipulations.GetDataFromServer($"account/{guid}"));
            Assert.Equal(account.Guid, acc.Guid);
            Assert.Equal(account.Name, acc.Name);
            Assert.Equal(account.Password, acc.Password);
            Assert.Equal(account.Age, acc.Age);
            Assert.Equal(account.Email, acc.Email);
        }
    }
}
