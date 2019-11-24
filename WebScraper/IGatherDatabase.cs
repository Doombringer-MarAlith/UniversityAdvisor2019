using System.Collections.Generic;
using Models;

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
