using Models.Models;
using System.Collections.Generic;
using System.Windows.Forms;

namespace App.FormManagers
{
    public interface IReadReviewFormManager : IBaseFormManager
    {
        string GetNameOfReview();

        string GetReviewText();

        void LoadNextOrPreviousReview(bool next, Form form);

        List<Review> GetCurrentReviewListBySubject();

        void ResetSelectedFaculty();

        void ResetReviewIndex();
    }
}