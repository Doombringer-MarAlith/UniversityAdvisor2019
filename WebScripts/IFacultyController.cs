using Models.Models;

namespace WebScripts
{
    public interface IFacultyController
    {
        string Get(string uniGuid);
        void Post(Faculty faculty);
    }
}