using System.Threading.Tasks;
using System.Windows.Forms;

namespace App.FormManagers
{
    public interface IWriteReviewFormManager : IBaseFormManager
    {
        Task SubmitReview(string text, int value, Form reviewForm);
        void ResetSelectedFaculty();
    }
}