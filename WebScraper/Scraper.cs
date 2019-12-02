using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Text;

namespace WebScraper
{
    public class Scraper : IGatherDatabase
    {
        private string _websiteLink { get; set; }
        private int _standardTimeout { get; set; }
        private const int _readThisMany = 880;
        private readonly HttpClient _client = new HttpClient();
        private string currentCountryName;
        private readonly string projectPath;
        private readonly List<List<string>> universityLinks = new List<List<string>>();
        private List<University> universities = new List<University>();
        private List<Faculty> faculties = new List<Faculty>();
        private List<Programme> programmes = new List<Programme>();
        private List<int> facultiesCountPerUniversity = new List<int>();
        private List<int> programmesCountPerFaculty = new List<int>();

        // Gets universities from WHED.net website.
        // Feed it html source file of uni search by country and it will gather Uni names + Uni descriptions + Faculties + Faculty programmes
        // Will need to incorporate adding items straight to database
        public Scraper(bool productionEnvironment)
        {
            _websiteLink = "https://www.whed.net/";
            _standardTimeout = 15000;

            if (productionEnvironment)
            {
                projectPath = AppDomain.CurrentDomain.BaseDirectory + "bin\\CountryLinks";
            }
            else
            {
                projectPath = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("Webserver"))
                    + "WebScraper\\CountryLinks";
            }
        }

        public Scraper(string websiteLink, int standardTimeout)
        {
            _websiteLink = websiteLink;
            _standardTimeout = standardTimeout;
        }

        public void UpdateData()
        {
            //TODO updating data in existing files in CountryLinks folder
        }

        // Returns true if success and false otherwise
        public bool TryToGatherUnversities()
        {
            DateTime startTime = DateTime.Now;
            string[] files = null;
            if ((files = GetFiles()) == null)
            {
                return false;
            }

            // Scrapes university links from every file (file contains up to 880 universities from WHED.net search by Country)
            foreach (var file in files)
            {
               if (file.Equals(projectPath + "\\Lithuania.txt"))
               {
                    try
                    {
                        using (File.OpenRead(file))
                        {
                            universityLinks.Add(ScrapeUniversityLinks(File.ReadAllText(file)));
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                    }

                    break;
               }
            }
            
            // Thread t = new Thread(() => StartThreadWithTimeout("", _standardTimeout)); currently unused
            // Thread t2 = new Thread(() => StartThreadWithTimeout("", _standardTimeout));
            int universityIndexInCountry;
            int currentCountryNum = 1;
            string previousCountryName = "";
            foreach (var linksList in universityLinks)
            {
                currentCountryName = files[currentCountryNum - 1].Substring(files[currentCountryNum - 1].IndexOf("CountryLinks", 0) + 13,
                    files[currentCountryNum - 1].Length - files[currentCountryNum - 1].IndexOf("CountryLinks", 0) - 17);

                if (previousCountryName == currentCountryName + "1" || previousCountryName == currentCountryName + "2")
                {
                    currentCountryName = previousCountryName;
                }

                universityIndexInCountry = 0;
                Console.Write("COUNTRY {0:d}/{1:d} ", currentCountryNum, universityLinks.Count);
                foreach (var link in linksList)
                {
                    using (WebClient client = new WebClient())
                    {
                        try
                        {
                            string htmlCode = client.DownloadString(_websiteLink + link);
                            Console.WriteLine("UNIVERSITY {0:d}/{1:d} START:" + DateTime.Now, universityIndexInCountry + 1, linksList.Count);
                            
                            /* Palieku komentare nes maybe ateity dar bandysiu padaryt kad su threadais veiktų
                            if (_currentUniversityId % 2 == 0)
                            {
                                if (!t.IsAlive)
                                {
                                    t = new Thread(() => StartThreadWithTimeout(htmlCode, _standardTimeout, _currentUniversityId));
                                    t.Start();
                                }
                                else
                                {
                                    StartThreadWithTimeout(htmlCode, _standardTimeout, _currentUniversityId);
                                }

                                if (universityIndexInCountry == linksList.Count - 1)
                                {
                                    t.Join();
                                }
                            }
                            else
                            {
                                if (!t2.IsAlive)
                                {
                                    t2 = new Thread(() => StartThreadWithTimeout(htmlCode, _standardTimeout, _currentUniversityId));
                                    t2.Start();

                                    if (universityIndexInCountry == linksList.Count - 1)
                                    {
                                        t2.Join();
                                    }
                                }
                                else
                                {
                                    StartThreadWithTimeout(htmlCode, _standardTimeout, _currentUniversityId);
                                }

                                if (t.IsAlive)
                                {
                                    t.Join();
                                }

                                if (t2.IsAlive)
                                {
                                    t2.Join();
                                }
                            }*/
                            
                            StartThreadWithTimeout(htmlCode, _standardTimeout);
                        }
                        catch (WebException e)
                        {
                            Console.WriteLine(e.StackTrace);
                            Console.WriteLine(_websiteLink + link);
                        }
                    }

                    universityIndexInCountry++;
                }
                previousCountryName = currentCountryName;
                currentCountryNum++;
            }

            Console.WriteLine("FINISHED in " + (DateTime.Now - startTime).TotalSeconds);
            return true;
        }

        private string[] GetFiles()
        {
            try
            {
                return Directory.GetFiles(projectPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// Finds links to all universities in given HTML file text and returns them as List<string>
        /// </summary>
        private List<string> ScrapeUniversityLinks(string text)
        {
            int startIndex = 0;
            int endIndex;
            List<string> universityLinks = new List<string>();
            string link;

            do
            {
                startIndex = text.IndexOf("detail_institution.php", startIndex, text.Length - startIndex) + 1;
                if (startIndex != 0)
                {
                    endIndex = text.IndexOf("\"", startIndex, text.Length - startIndex - 1);
                    link = text.Substring(startIndex - 1, endIndex - startIndex + 1);
                    universityLinks.Add(link);
                }
            } while (startIndex != 0);

            return universityLinks;
        }

        private string FindElement(string text, string begin, string ending, int offset)
        {
            int start;
            if ((start = text.IndexOf(begin) + begin.Length + offset) == -1 + begin.Length + offset)
            {
                return null;
            }

            int end = text.IndexOf(ending, start);
            if (end == -1)
            {
                return null;
            }

            byte[] bytes = Encoding.Default.GetBytes(text.Substring(start, end - start));
            return Encoding.UTF8.GetString(bytes);
        }

        // Find info about university and request faculty info
        private void ScrapeUniversity(string text)
        {
            University university = new University()
            {
                Country = currentCountryName,
                Name = FindElement(text, "<h2>", "<span", 0).Trim(new char[] { '\t', '\n', ' ' }).Replace("&ndash; ", ""),
                City = FindElement(text, "City:</span>", "</span>", 22),
                Description = FindElement(text, "<span class=\"dt\">History", "</sp", 59)
            };

            universities.Add(university);
            ReadFaculties(text);
        }

        private void ReadFaculties(string text)
        {
            int start = 0;
            int end;
            string facultyName;
            int nextFaculty, nextFOS;
            int facultyCount = 0;

            do
            {
                List<string> fields;
                start = text.IndexOf("Faculty : ", start);
                if (start != -1) {
                    start += 10;
                    end = text.IndexOf("</p>", start);
                    byte[] bytes = Encoding.Default.GetBytes(text.Substring(start, end - start));
                    facultyName = Encoding.UTF8.GetString(bytes);
                    faculties.Add(new Faculty { Name = facultyName});
                    facultyCount++;

                    // Searching for fields of study
                    nextFaculty = text.IndexOf("Faculty : ", start) > 0 ? text.IndexOf("Faculty : ", start) + 10 : text.Length; //doesn't exist -> good too
                    nextFOS = text.IndexOf("Fields of study:", start) + 45;
                    if (nextFaculty > nextFOS && nextFOS != 44) // 44 as in returned -1(not found) + 45 (line above)
                    {
                        end = text.IndexOf("</span>", nextFOS);
                        bytes = Encoding.Default.GetBytes(text.Substring(nextFOS, end - nextFOS));
                        fields = Regex.Split(Encoding.UTF8.GetString(bytes), ", ").ToList();

                        foreach (var field in fields)
                        {
                            programmes.Add(new Programme() { Name = field });
                        }

                        programmesCountPerFaculty.Add(fields.Count());
                    }
                }

            } while (start != -1);

            facultiesCountPerUniversity.Add(facultyCount);
        }

        /// <summary>
        /// Gets country's university's href list
        /// </summary>
        /// <param name="country">Country's name to get university list for</param>
        /// <param name="offset">From what number to start gathering university hrefs (2 to 2 + _readThisMany)</param>
        /// <returns></returns>
        private async Task<int> Scrape(string country, int offset)
        {
            var values = new Dictionary<string, string>
            {
            { "Chp1", country },
            { "membre", "0" },
            { "nbr_ref_pge", _readThisMany.ToString() },
            { "debut", offset.ToString() }
            };

            var content = new FormUrlEncodedContent(values);

            var response = await _client.PostAsync(_websiteLink + "results_institutions.php", content);

            var responseString = await response.Content.ReadAsStringAsync();

            try
            {
                using (StreamWriter outputFile = new StreamWriter(projectPath + "\\" + country + ".txt")) 
                {
                    outputFile.Write(responseString);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return FindUniversityCount(responseString);
        }

        private int FindUniversityCount(string text)
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

        private void StartThreadWithTimeout(string text, int timeout)
        {
            Thread t = new Thread(() => ScrapeUniversity(text));
            t.Start();
            DateTime current = DateTime.Now;
            while (t.IsAlive)
            {
                if ((DateTime.Now - current).TotalMilliseconds > timeout)
                    break;
            }

            if (t.IsAlive)
            {
                Console.WriteLine("Thread aborted");
                t.Abort();
            }
        }

        private void WriteToFile(string txt, string path)
        {
            try
            {
                using (StreamWriter writetext = File.AppendText(path))
                {
                    writetext.WriteLine(txt);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw;
            }
        }

        private void CleanupHtml(string outputPath, string[] files)
        {
            string path = outputPath;
            int currentCountryNum = 1;
            foreach (var listForCountry in universityLinks)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(path + files[currentCountryNum - 1].Substring(files[currentCountryNum - 1].IndexOf("CountryLinks", 0) + 13,
                    files[currentCountryNum - 1].Length - files[currentCountryNum - 1].IndexOf("CountryLinks", 0) - 17) + ".txt"))
                    {
                        foreach (var link in listForCountry)
                        {
                            writer.Write(link + "\",\r\n");
                        }
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    throw;
                }

                currentCountryNum++;
            }
        }

        public List<University> GetUniversities()   
        {
            return universities;
        }

        public List<Faculty> GetFaculties()
        {
            return faculties;
        }

        public List<Programme> GetProgrammes()
        { 
            return programmes;  
        }

        public List<int> GetFacultiesCountPerUniversity() 
        { 
            return facultiesCountPerUniversity;
        }

        public List<int> GetProgrammesCountPerFaculty()
        { 
            return programmesCountPerFaculty;
        }
    }
}