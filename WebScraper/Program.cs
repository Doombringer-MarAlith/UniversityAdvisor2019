using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using Models;
using System.Text.RegularExpressions;
using System.Threading;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebScraper
{
    class Program
    {
        static List<University> ScrapedUniversities = new List<University>();
        static List<Faculty> _faculties = new List<Faculty>();
        const string websiteLink = "https://www.whed.net/";
        private static readonly HttpClient client = new HttpClient();
        const int readThisMany = 880;
        static string CountryLinksPath = "CountryLinks";

        // gets universities from WHED.net website.
        // Feed it html source file of uni search by country and it will gather Uni names + Uni descriptions + Faculties + Faculty programmes
        // Will need to incorporate adding items straight to database

        static void Main(string[] args)
        {
            DateTime startTime = DateTime.Now;
            List<List<string>> universityLinks = new List<List<string>>();
            string[] files = null;
            try
            {
                files = Directory.GetFiles(CountryLinksPath); // CountryLinksPath  
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return;
            }

            // scrapes university links from every file (file contains up to 100 universities from WHED.net search by Country)
            foreach (var file in files)
            {
                try
                {
                    using (File.OpenRead(file))
                    {
                        universityLinks.Add(ScrapeUniversityLinks(File.ReadAllText(file)));
                    }
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }

            Thread t;
            Thread t2;
            int current;
            int currentCountryNum = 1;
            foreach (var linksList in universityLinks)
            {
                current = 0;
                Console.Write("COUNTRY {0:d}/{1:d} ", currentCountryNum, universityLinks.Count);
                foreach (var link in linksList)
                {
                    using (WebClient client = new WebClient())
                    {
                        try
                        {
                            string htmlCode = client.DownloadString(websiteLink + link);
                            Console.WriteLine("UNIVERSITY {0:d}/{1:d} START:" + DateTime.Now, current + 1, linksList.Count);

                            if (current % 3 == 0)
                            {
                                t = new Thread(() => ScrapeUniversity(htmlCode));
                                t.Start();
                                if (current == linksList.Count - 1)
                                {
                                    t.Join();
                                }
                            }
                            else if (current % 2 == 0)
                            {
                                t2 = new Thread(() => ScrapeUniversity(htmlCode));
                                t2.Start();
                                if (current == linksList.Count - 1)
                                {
                                    t2.Join();
                                }
                            }
                            else
                            {
                                ScrapeUniversity(htmlCode);
                            }
                        }
                        catch (WebException e)
                        {
                            Console.WriteLine(e.StackTrace);
                            Console.WriteLine("https://www.whed.net/" + link);
                        }
                    }
                    current++;
                }
                currentCountryNum++;
            }
            Console.WriteLine("FINISHED in " + (DateTime.Now - startTime).TotalSeconds);
            Console.ReadLine();
        }

        // Find links to all universities in given HTML file text
        static List<string> ScrapeUniversityLinks(string text)
        {
            int startIndex = 0;
            int endIndex;
            List<string> universityLinks = new List<string>();
            string link;
            do
            {
                startIndex = text.IndexOf("detail_institution.php", startIndex, text.Length - startIndex - 1) + 1;
                if (startIndex != 0)
                {
                    endIndex = text.IndexOf("\"", startIndex, text.Length - startIndex - 1);
                    link = text.Substring(startIndex - 1, endIndex - startIndex + 1);
                    universityLinks.Add(link);
                }
            } while (startIndex != 0);
            return universityLinks;
        }

        // Find the name of university and it's Faculties' names
        static void ScrapeUniversity(string text)
        {
            University university = new University();

            // Read University name
            int start = text.IndexOf("<h2>") + 4; // 4 length
            int end = text.IndexOf("<span", start);

            // while(notNewUniversityGuid) DO {generate new guid}
            university.Name = text.Substring(start, end - start).Replace("\n", "").Replace("\t", ""); // Generate University Guid and save

            // Read University description
            start = text.IndexOf("<span class=\"dt\">History") + 83;
            if (start != 82)
            {
                end = text.IndexOf("</sp", start);
                university.Description = text.Substring(start, end - start);
            }
            ScrapedUniversities.Add(university);
            ReadFaculties(text, "fakeID");
        }

        static void ReadFaculties(string text, string universityId)
        {
            int start = 0;
            int end;
            string facultyName;
            int nextFaculty, nextFOS;
            do
            {
                List<string> fields;
                List<Programme> programmes = new List<Programme>();
                start = text.IndexOf("Faculty : ", start);
                string facultyGuid;
                if (start != -1)
                {
                    start += 10;
                    end = text.IndexOf("</p>", start);
                    facultyName = text.Substring(start, end - start);

                    // while(notNewFacultyGuid) DO {generate new guid}
                    _faculties.Add(new Faculty() { Name = facultyName }); // Add Guid(and save) and University Guid from above

                    // Searching for fields of study
                    nextFaculty = text.IndexOf("Faculty : ", start) > 0 ? text.IndexOf("Faculty : ", start) + 10 : text.Length; //doesn't exist -> good too
                    nextFOS = text.IndexOf("Fields of study:", start) + 45;
                    if (nextFaculty > nextFOS && nextFOS != 44) // 44 as in returned -1(not found) + 45 (line above)
                    {
                        end = text.IndexOf("</span>", nextFOS);
                        fields = Regex.Split(text.Substring(nextFOS, end - nextFOS), ", ").ToList();

                        foreach (var field in fields)
                        {
                            // while(notNewProgrammeGuid) DO {generate new guid}
                            programmes.Add(new Programme() { Name = field }); // Guid create needed. Also add Faculty's from above Guid
                        }
                    }
                }
            } while (start != -1);
        }

        // gets country's university's link list
        static async Task<int> Scrape(string country, int readThisMany, int offset)
        {
            var values = new Dictionary<string, string>
            {
            { "Chp1", country },
            { "membre", "0" },
            { "nbr_ref_pge", readThisMany.ToString() },
            { "debut", offset.ToString() }
            };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync(websiteLink + "results_institutions.php", content);

            var responseString = await response.Content.ReadAsStringAsync();

            using (StreamWriter outputFile = new StreamWriter(@"C:\Users\nihilistic\Desktop\countryLinks\" + country + ".txt")) // CountryLinksPath + "\\" + country + ".txt"
            {
                outputFile.Write(responseString);
            }
            return FindUniversityCount(responseString);
        }

        static int FindUniversityCount(string text)
        {
            int start = text.IndexOf("total") + 14;
            if (start == 13)
            {
                return -1;
            }
            int endIndex = text.IndexOf("\">", start);
            if (endIndex - start < 1)
            {
                return -1;
            }
            string txt = text.Substring(start, endIndex - start);
            return Int32.Parse(txt);
        }
    }
}
