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
                List<int> facultyCount = gatherDatabase.GetFacultiesCountPerUniversity();
                List<int> programmeCount = gatherDatabase.GetProgrammesCountPerFaculty();
                int currentIndex = 0;
                int id = 1;

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
                    foreach (var currentCount in facultyCount)
                    {
                        for (int i = 0; i < currentCount; i++)
                        {
                            faculties[currentIndex].UniversityId = id;
                            dbContext.Faculties.Add(faculties[currentIndex]);
                            currentIndex++;
                        }
                        id++;
                    }
                }

                id = 1;
                currentIndex = 0;

                if (!dbContext.Programmes.Any())
                {
                    foreach (var currentCount in programmeCount)
                    {
                        for (int i = 0; i < currentCount; i++)
                        {
                            programmes[currentIndex].FacultyId = id;
                            dbContext.Programmes.Add(programmes[currentIndex]);
                            currentIndex++;
                        }
                        id++;
                    }
                }

                dbContext.SaveChanges();
            }
        }
    }
}