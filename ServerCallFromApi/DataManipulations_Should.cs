using System.Net;
using System.Net.Http;
using AutoFixture;
using Moq;
using Xunit;

namespace ServerCallFromApp.UnitTests
{
    public class DataManipulations_Should
    {
        private const string Url = "https://localhost:44380/api/";

        [Theory]
        [InlineData("oomp")]
        public void GetDataFromServer_CallServerWithCorrectValues(string url)
        {
            Mock<HttpClient> client = new Mock<HttpClient>();
            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK);
            client.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(message);
        }
    }
}
