using Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebScraper;

namespace Webserver.Data.Services
{
    public class DatabaseFiller : IDatabaseFiller
    {
        private readonly IGatherDatabase _gatherDatabase;

        public DatabaseFiller(IGatherDatabase gatherDatabase)
        {
            _gatherDatabase = gatherDatabase;
        }

        public async Task Fill(ApplicationDbContext dbContext)
        {
            if (_gatherDatabase.TryToGatherUniversities())
            {
                List<University> universities = _gatherDatabase.GetUniversities();
                List<Faculty> faculties = _gatherDatabase.GetFaculties();
                List<Programme> programmes = _gatherDatabase.GetProgrammes();
                List<int> facultyCount = _gatherDatabase.GetFacultiesCountPerUniversity();
                List<int> programmeCount = _gatherDatabase.GetProgrammesCountPerFaculty();
                int currentIndex = 0;
                int id = 1;

                foreach (var university in universities)
                {
                    dbContext.Universities.Add(university);
                }

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

                id = 1;
                currentIndex = 0;

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

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
