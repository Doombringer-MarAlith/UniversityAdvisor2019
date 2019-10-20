using System.Net.Http;
using System.Threading.Tasks;

namespace ExternalDependencies
{
    public interface IHttpInternalClient
    {
        Task<HttpResponseMessage> PostAsync(string uri, HttpContent content);
        Task<HttpResponseMessage> PutAsync(string uri, HttpContent content);
        Task<HttpResponseMessage> GetAsync(string uri);
    }
}