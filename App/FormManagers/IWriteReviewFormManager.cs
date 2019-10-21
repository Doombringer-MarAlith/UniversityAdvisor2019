using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public interface IWriteReviewFormManager
    {
        Task SubmitReview(string text, int value, Form reviewForm);
        void ResetSelectedFaculty();
    }
}