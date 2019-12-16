using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Webserver.Helpers
{
    public static class CountryStore
    {
        public static List<string> CountryNames { get; set; }

        public static void ReadCountryNamesFromFile(string path)
        {
            if (CountryNames == null)
            {
                CountryNames = new List<string>();
            }

            try
            {
                using (var streamReader = new StreamReader(path, Encoding.UTF8))
                {
                    string data = streamReader.ReadToEnd();
                    byte[] bytes = Encoding.UTF8.GetBytes(data);
                    CountryNames = Regex.Split(Encoding.UTF8.GetString(bytes), ",").ToList();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }

            CountryNames = CountryNames.OrderBy(countryName => countryName).ToList();
        }
    }
}