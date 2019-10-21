using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExternalDependencies
{
    public class HttpInternalClient : IHttpInternalClient, IDisposable
    {
        private readonly HttpClient _client = new HttpClient();

        public async Task<HttpResponseMessage> PostAsync(string uri, HttpContent content)
        {
            return await _client.PostAsync(uri, content);
        }

        public async Task<HttpResponseMessage> PutAsync(string uri, HttpContent content)
        {
            return await _client.PutAsync(uri, content);
        }

        public async Task<HttpResponseMessage> GetAsync(string uri)
        {
            return await _client.GetAsync(uri);
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}