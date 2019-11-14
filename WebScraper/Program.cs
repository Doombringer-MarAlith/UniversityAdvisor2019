using System;
using System.IO;
using Models.Models;
using System.Collections.Generic;
using System.Net;

namespace WebScraper
{
    class Program
    {

        static List<University> scrapedUniversities = new List<University>();
        static List<Faculty> faculties = new List<Faculty>();

        static void Main(string[] args)
        {
            var files = Directory.GetFiles("ScrapeFiles");
            List<List<string>> universityLinks = new List<List<string>>();
            int currentUni = 0;
            foreach(var file in files)
            {
                File.OpenRead(file);
                universityLinks.Add(ScrapeUniversityLinks(File.ReadAllText(file)));
            }

            

            foreach (var linksList in universityLinks)
            {
                foreach (var link in linksList)
                {
                    using (WebClient client = new WebClient())
                    {
                        string htmlCode = client.DownloadString("https://www.whed.net/" + link);
                        scrapedUniversities.Add(ScrapeUniversity(htmlCode));
                        Console.WriteLine(scrapedUniversities[scrapedUniversities.Count - 1].Name);

                        for (int i = currentUni; i < faculties.Count; i++)
                        {
                            Console.WriteLine(faculties[i].Name);
                        }
                        currentUni = faculties.Count;
                        Console.WriteLine("\r\n");

                    }
                }
                Console.WriteLine("\r\n");
            }
        }

        // Find links to all universities
        static List<string> ScrapeUniversityLinks(string text)
        {
            int ans = 0;
            int endIndex;
            List<string> uniLinks = new List<string>();
            string link;
            do
            {
                ans = text.IndexOf("detail_institution.php", ans, text.Length-ans-1)+1;
                if (ans != 0)
                {
                    endIndex = text.IndexOf("\"", ans, text.Length - ans - 1);
                    link = text.Substring(ans - 1, endIndex - ans + 1);
                    uniLinks.Add(link);
                }
            } while (ans != 0);
            return uniLinks;
        }

        // Find the name of university and it's divisions'(faculties) names
        static University ScrapeUniversity(string text)
        {
            University university = new University();

            // Read University name
            int start = text.IndexOf("<h2>")+4; // 4 length
            int end = text.IndexOf("<span", start);
            university.Name = text.Substring(start, end - start).Replace("\n", "").Replace("\t", "");

            // Read Divisions (Faculties)
            start = 0;
            string facultyName;

            do
            {
                start = text.IndexOf("Faculty : ", start);
                if (start != -1)
                {
                    start += 10;
                    end = text.IndexOf("</p>", start);
                    facultyName = text.Substring(start, end - start);
                    faculties.Add(new Faculty() { Name = facultyName });
                }
            } while (start != -1); // Po sito pridet Fields of study (courses) listus

            Console.WriteLine(faculties.Count);

            return university;
        }
    }
}
