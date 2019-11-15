﻿using System;
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

        // gets universities from WHED.net website.
        // Feed it html source file of uni search by country and it will gather Uni names + Faculties

        // Subjects for faculties to be added
        // Will need to incorporate adding items straight to database
        // HTML also has 'History' that could be used as description

        static void Main(string[] args)
        {
            var files = Directory.GetFiles("ScrapeFiles");
            List<List<string>> universityLinks = new List<List<string>>();
            int currentUniversity = 0;
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
                        Console.WriteLine(scrapedUniversities[scrapedUniversities.Count - 1].Name); // part below in these foreaches is only for debug

                        for (int i = currentUniversity; i < faculties.Count; i++)
                        {
                            Console.WriteLine(faculties[i].Name);
                        }
                        currentUniversity = faculties.Count;
                        Console.WriteLine("\r\n");
                    }
                }
                Console.WriteLine("\r\n");
            }
        }

        // Find links to all universities in given HTML file text
        static List<string> ScrapeUniversityLinks(string text)
        {
            int answer = 0;
            int endIndex;
            List<string> universityLinks = new List<string>();
            string link;
            do
            {
                answer = text.IndexOf("detail_institution.php", answer, text.Length-answer-1)+1;
                if (answer != 0)
                {
                    endIndex = text.IndexOf("\"", answer, text.Length - answer - 1);
                    link = text.Substring(answer - 1, endIndex - answer + 1);
                    universityLinks.Add(link);
                }
            } while (answer != 0);
            return universityLinks;
        }

        // Find the name of university and it's divisions'(faculties) names
        static University ScrapeUniversity(string text)
        {
            University university = new University();

            // Read University name
            int start = text.IndexOf("<h2>")+4; // 4 length
            int end = text.IndexOf("<span", start);
            university.Name = text.Substring(start, end - start).Replace("\n", "").Replace("\t", ""); // Generate University Guid and save

            // Read Divisions (Faculties)
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
                    if(nextFaculty > nextFOS && nextFOS != 44) // 44 as in returned -1(not found) + 45 line above
                    {
                        end = text.IndexOf("</span>", nextFOS);
                        fields = text.Substring(nextFOS, end - nextFOS).Split(", ").ToList();

                        foreach (var field in fields)
                        {
                            programmes.Add(new Programme() { Name = field }); // Guid create needed. Also add Faculty's above Guid
                            Console.Write(field + " ");
                        }
                        Console.WriteLine("\r\n");
                    }

                }
            } while (start != -1);
            
            Console.WriteLine(faculties.Count);
            return university;
        }
    }
}
