using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ExternalDependencies;

namespace ServerCallFromApp
{
    public class DataManipulations : IDataManipulations
    {
        private const string Url = "https://localhost:44312/api/";
        private readonly IHttpInternalClient _client;

        public DataManipulations(IHttpInternalClient client)
        {
            _client = client;
        }

        public async Task<string> GetDataFromServer(string url)
        {
            HttpResponseMessage response = await _client.GetAsync(Url + url);

            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var dataObjects = response.Content.ReadAsStringAsync().Result;
                return dataObjects;
            }

            return null;
        }

        public async Task PostDataToServer(string url, string data)
        {
            var payload = data;
            HttpContent httpContent = new StringContent(payload, Encoding.UTF8, "application/json");
            await _client.PostAsync(Url + url, httpContent);
        }
    }
}