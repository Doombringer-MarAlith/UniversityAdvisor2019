using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace WebScraper
{
    class Test
    {

        // In order for this demo to quickly, you'll have to modify the Scraper.cs a little

        // add if statement under "foreach (var file in files)" on LINE 56 (or around there if it changes)
        // "if(file.Equals("CountryLinks\\Lithuania.txt"))"

        // all of the content of that foreach should be inside if statement's body
        // at the end of the foreach content, before the closing bracket of if body, add "break;" (to only gather Lithuania's universities)
        // now run and lithuania's university names should come up eventually
        static void Main()
        {
            IGatherDatabase scraper = new Scraper();
            if (scraper.GatherUnversities())
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
