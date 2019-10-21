using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public interface IUniversitySearchFormManager
    {
        Task<List<string>> GetUniversities(string name);
        void OpenSelectedUniversityForm(int selectedUniversityIndex, Form form);
    }
}