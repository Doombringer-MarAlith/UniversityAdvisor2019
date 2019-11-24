using System.Collections.Generic;
using Models;

namespace WebScraper
{
    public interface IGatherDatabase
    {
        bool GatherUnversities();
        void UpdateData();
        List<University> GetUniversities();
        List<Faculty> GetFaculties();
        List<Programme> GetProgrammes();
    }
}
