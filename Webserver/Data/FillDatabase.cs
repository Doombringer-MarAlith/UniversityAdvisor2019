using System.Collections.Generic;
using Models;
using System.Linq;
using WebScraper;
using System;

namespace Webserver.Data
{
    public static class FillDatabase
    {
        public static void Initialize(ApplicationDbContext dbContext, IGatherDatabase gatherDatabase)
        {
            if (gatherDatabase.TryToGatherUnversities())
            {
                List<University> universities = gatherDatabase.GetUniversities();
                List<Faculty> faculties = gatherDatabase.GetFaculties();
                List<Programme> programmes = gatherDatabase.GetProgrammes();

                if (!dbContext.Universities.Any())
                {
                    foreach (var university in universities)
                    {
                        university.FoundingDate = DateTime.Now;
                        dbContext.Universities.Add(university);
                    }
                }

                if (!dbContext.Faculties.Any())
                {
                    foreach (var faculty in faculties)
                    {
                        dbContext.Faculties.Add(faculty);
                    }
                }

                if (!dbContext.Programmes.Any())
                {
                    foreach (var programme in programmes)
                    {
                        dbContext.Programmes.Add(programme);
                    }
                }

                    dbContext.SaveChanges();
            }
        }
    }
}