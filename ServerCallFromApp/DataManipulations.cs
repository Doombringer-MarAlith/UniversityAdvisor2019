using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServerCallFromApp
{
    public class DataManipulations : IDataManipulations
    {
        private const string Url = "https://localhost:44380/api/";
        private readonly HttpClient _client;

        public DataManipulations(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> GetDataFromServer(string url)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(Url + url);

            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var dataObjects = response.Content.ReadAsStringAsync().Result;
                client.Dispose();
                return dataObjects;
            }

            client.Dispose();
            return null;
        }

        public async Task PostDataToServer(string url, string data)
        {

            var payload = data;
            HttpContent httpContent = new StringContent(payload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(Url + url, httpContent);
            _client.Dispose();
        }
    }
}
