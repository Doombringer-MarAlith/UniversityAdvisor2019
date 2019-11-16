using System;
using System.IO;
using Models.Models;
using System.Collections.Generic;
using System.Net;
using System.Collections;
using System.Linq;

namespace WebScraper
{
    class Program
    {

        static List<University> scrapedUniversities = new List<University>();
        static List<Faculty> faculties = new List<Faculty>();
        static int programmeSum = 0;

        // UNCOMMENT Console.writeline lines to see results

        // gets universities from WHED.net website.
        // Feed it html source file of uni search by country and it will gather Uni names + Uni descriptions + Faculties + Faculty programmes

        // Will need to incorporate adding items straight to database

        static void Main(string[] args)
        {
            var files = Directory.GetFiles("ScrapeFiles");
            List<List<string>> universityLinks = new List<List<string>>();
            int currentUniversity = 0;
            
            // scrapes university links from every file (file contains up to 100 universities from WHED.net search by Country)
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

                        // part below in these foreaches is only for debug
                        /*
                        Console.WriteLine(scrapedUniversities[scrapedUniversities.Count - 1].Name); 
                        for (int i = currentUniversity; i < faculties.Count; i++)
                        {
                            Console.WriteLine(faculties[i].Name);
                        }
                        currentUniversity = faculties.Count;
                        Console.WriteLine("\r\n");
                        */
                    }
                }
                //Console.WriteLine("\r\n");
            }
            //Console.WriteLine(programmeSum);
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
                startIndex = text.IndexOf("detail_institution.php", startIndex, text.Length-startIndex-1) + 1;
                if (startIndex != 0)
                {
                    endIndex = text.IndexOf("\"", startIndex, text.Length - startIndex - 1);
                    link = text.Substring(startIndex - 1, endIndex - startIndex + 1);
                    universityLinks.Add(link);
                }
            } while (startIndex != 0);
            return universityLinks;
        }

        // Find the name of university and it's faculties' names
        static University ScrapeUniversity(string text)
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
                // Console.WriteLine(university.Description + "\r\n");
            }

            // Read Faculties
            start = 0;
            string facultyName;
            int nextFaculty, nextFOS;
            do
            {
                List<string> fields = new List<string>();
                List<Programme> programmes = new List<Programme>();
                start = text.IndexOf("Faculty : ", start);
                string facultyGuid = Guid.NewGuid().ToString();
                if (start != -1)
                {
                    start += 10;
                    end = text.IndexOf("</p>", start);
                    facultyName = text.Substring(start, end - start);

                    // while(notNewFacultyGuid) DO {generate new guid}
                    faculties.Add(new Faculty() { Name = facultyName }); // Add Guid(and save) and University Guid from above

                    // Searching for fields of study
                    nextFaculty = text.IndexOf("Faculty : ", start) > 0 ? text.IndexOf("Faculty : ", start) + 10 : text.Length; //doesn't exist -> good too
                    nextFOS = text.IndexOf("Fields of study:", start) + 45;
                    if(nextFaculty > nextFOS && nextFOS != 44) // 44 as in returned -1(not found) + 45 (line above)
                    {
                        end = text.IndexOf("</span>", nextFOS);
                        fields = text.Substring(nextFOS, end - nextFOS).Split(", ").ToList();

                        foreach (var field in fields)
                        {
                            // while(notNewProgrammeGuid) DO {generate new guid}
                            programmes.Add(new Programme() { Name = field }); // Guid create needed. Also add Faculty's above Guid
                            //Console.Write(field + " ");
                        }
                        //Console.WriteLine("\r\n");
                        programmeSum += programmes.Count;
                    }
                }
            } while (start != -1);

            // Console.WriteLine(faculties.Count);
            return university;
        }
    }
}
