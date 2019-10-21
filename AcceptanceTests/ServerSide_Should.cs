using AcceptanceTests.TestHelpers;
using Models.Models;
using Newtonsoft.Json;
using ServerCallFromApp;
using System.Net.Http;
using System.Threading.Tasks;
using ExternalDependencies;
using Xunit;

namespace AcceptanceTests
{
    public class ServerSide_Should
    {
        [Theory]
        [AutoMoqData]
        public async Task Create_Login_AndGetAccount(Account account)
        {
            DataManipulations dataManipulations = new DataManipulations(new HttpInternalClient());

            await dataManipulations.PostDataToServer("account/create", JsonConvert.SerializeObject(account));
            var returnGuid = await dataManipulations.GetDataFromServer($"account/login/{account.Email}/{account.Password}");
            Assert.NotNull(returnGuid);
            Assert.Equal(account.Guid, returnGuid);

            var fetchedAccount = JsonConvert.DeserializeObject<Account>(await dataManipulations.GetDataFromServer($"account/{account.Guid}"));
            Assert.Equal(account.Guid, fetchedAccount.Guid);
            Assert.Equal(account.Name, fetchedAccount.Name);
            Assert.Equal(account.Password, fetchedAccount.Password);
            Assert.Equal(account.Email, fetchedAccount.Email);
        }
    }
}