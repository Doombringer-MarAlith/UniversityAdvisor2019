using System;
using System.Collections.Generic;
using System.IO;

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
                using (File.OpenRead(path))
                {
                    string data = File.ReadAllText(path);
                    string[] countries = data.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string country in countries)
                    {
                        CountryNames.Add(country);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}