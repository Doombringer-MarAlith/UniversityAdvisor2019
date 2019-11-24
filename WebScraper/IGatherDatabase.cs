using Models;
using System.Collections.Generic;

namespace WebScraper
{
    public interface IGatherDatabase
    {
        bool TryToGatherUnversities();

        void UpdateData();

        List<University> GetUniversities();

        List<Faculty> GetFaculties();

        List<Programme> GetProgrammes();
    }
}