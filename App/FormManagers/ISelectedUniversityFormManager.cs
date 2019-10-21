using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public interface ISelectedUniversityFormManager
    {
        void OpenWriteReviewFormForSelectedFaculty(int selectedFacultyIndex, Form form);
        void OpenWriteReviewFormForSelectedUniversity(Form form);
        Task LoadReviewsForSelectedUniversity(Form form);
        Task LoadReviewsForSelectedFaculty(int selectedFacultyIndex, Form form);
        Task<List<string>> GetFaculties();
        string GetSelectedUniversityName();
        void CloseSelectedUniversityForm(Form form);
        void ResetSelectedUniversity();
        void ResetSelectedFaculty();
    }
}