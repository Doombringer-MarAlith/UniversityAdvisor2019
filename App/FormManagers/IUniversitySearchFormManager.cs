using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Objektinis.FormManagers;

namespace App
{
    public interface IUniversitySearchFormManager : IBaseFormManager
    {
        Task<List<string>> GetUniversities(string name);
        void OpenSelectedUniversityForm(int selectedUniversityIndex, Form form);
    }
}