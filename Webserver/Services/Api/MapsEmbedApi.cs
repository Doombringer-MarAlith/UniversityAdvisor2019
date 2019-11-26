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
            result.Append(MakeStringUrlCompatible(seachCriteria));

            return result.ToString();
        }

        private string MakeStringUrlCompatible(string value)
        {
            StringBuilder result = new StringBuilder();

            foreach (string key in value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
            {
                result.Append(key + '+');
            }

            result.Remove(result.Length - 1, 1);
            return result.ToString();
        }
    }
}