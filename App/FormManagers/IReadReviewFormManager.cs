using System.Collections.Generic;
using System.Windows.Forms;
using Models.Models;

namespace App
{
    public interface IReadReviewFormManager
    {
        string GetNameOfReviewee();
        string GetReviewText();
        void LoadNextOrPreviousReview(bool next, Form form);
        List<Review> GetCurrentReviewListBySubject();
        void ResetSelectedFaculty();
    }
}