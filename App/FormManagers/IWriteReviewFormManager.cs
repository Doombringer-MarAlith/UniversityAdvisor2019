using System.Threading.Tasks;
using System.Windows.Forms;
using App;

namespace Objektinis.FormManagers
{
    public interface IWriteReviewFormManager : IBaseFormManager
    {
        Task SubmitReview(string text, int value, Form reviewForm);
        void ResetSelectedFaculty();
    }
}