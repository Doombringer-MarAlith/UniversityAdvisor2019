using System.Collections.Generic;
using System.Windows.Forms;
using App;
using Models.Models;

namespace Objektinis.FormManagers
{
    public interface IReadReviewFormManager : IBaseFormManager
    {
        string GetNameOfReview();
        string GetReviewText();
        void LoadNextOrPreviousReview(bool next, Form form);
        List<Review> GetCurrentReviewListBySubject();
        void ResetSelectedFaculty();
    }
}