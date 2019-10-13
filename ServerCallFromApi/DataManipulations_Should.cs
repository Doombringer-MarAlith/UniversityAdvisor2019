using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using Moq;
using ServerCallFromApp.UnitTests.Helper;
using Xunit;

namespace ServerCallFromApp.UnitTests
{
    public class DataManipulations_Should
    {
    //   private readonly IDataManipulations _dataManipulations;
        private const string Url = "https://localhost:44380/api/";
        [Theory]
        [AutoMoqData]
        public async Task GetDataFromServer_CallServerWithCorrectValues([Frozen] Mock<HttpClient> client, DataManipulations dataManipulations)
        {
            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK);
            await dataManipulations.PostDataToServer("url","data");
            client.Verify(x => x.PostAsync(Url+"url",It.IsAny<HttpContent>()),Times.Once());
        }
    }
}
