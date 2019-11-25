using Models;
using System;
using System.Collections.Generic;

namespace WebScraper
{
    internal class Test
    {
        // In order for this demo to quickly, you'll have to modify the Scraper.cs a little

        // add if statement under "foreach (var file in files)" on LINE 56 (or around there if it changes)
        // "if(file.Equals("CountryLinks\\Lithuania.txt"))"

        // all of the content of that foreach should be inside if statement's body
        // at the end of the foreach content, before the closing bracket of if body, add "break;" (to only gather Lithuania's universities)
        // now run and lithuania's university names should come up eventually
        private static void Main()
        {
            IGatherDatabase scraper = new Scraper();
            if (scraper.TryToGatherUnversities())
            {
                List<University> unis = scraper.GetUniversities();
                foreach (var item in unis)
                {
                    Console.WriteLine(item.Name);
                }
            }
            Console.ReadKey();
        }
    }
}