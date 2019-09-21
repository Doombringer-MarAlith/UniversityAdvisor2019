using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;

namespace ServerCallFromApp
{
    public static class DataManipulations
    {
        private const string Url = "https://localhost:44380/api/";

        public static string GetDataFromServer(string url)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(Url+url).Result;

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
        public static void PostDataToServer(string url,string data)
        {
            HttpClient client = new HttpClient();
            var payload = data;

            HttpContent httpContent = new StringContent(payload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(Url + url,httpContent).Result;
            client.Dispose();
        }
    }
}