using System;
using System.Configuration;
using System.Text;

namespace Webserver.Services.Api
{
    public class MapsEmbedApi : IMapsApi
    {
        public string GetStaticMapUri(string seachCriteria)
        {
            StringBuilder result = new StringBuilder();

            result.Append(ConfigurationManager.AppSettings["GoogleMapsEmbedBaseUrl"].ToString());
            result.Append(ConfigurationManager.AppSettings["GoogleMapsEmbedApiKey"].ToString());
            result.Append("&q=");

            foreach (string key in seachCriteria.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
            {
                result.Append(key + '+');
            }

            result.Remove(result.Length - 1, 1);
            return result.ToString();
        }
    }
}