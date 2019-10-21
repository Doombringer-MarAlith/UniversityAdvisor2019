using Models.Models;
using Objektinis;
using ServerCallFromApp;
using System.Collections.Generic;
using System.Windows.Forms;

namespace App
{
    public class ReadReviewFormManager : BaseFormManager, IReadReviewFormManager
    {
        protected ReadReviewFormManager(IDataManipulations dataManipulations, FormManagerData formManagerData) : base(dataManipulations, formManagerData)
        {
        }

        // Returns the name of whatever is currently reviewed
        public string GetNameOfReviewee()
        {
            switch (FormManagerData.CurrentReviewSubject)
            {
                case ReviewType.REVIEW_UNIVERSITY:
                    return FormManagerData.SelectedUniversity.Name;

                case ReviewType.REVIEW_FACULTY:
                    return FormManagerData.SelectedFaculty.Name;

                default:
                    return "";
            }
        }

        // Returns text of current review or empty string if the boundaries are reached
        public string GetReviewText()
        {
            List<Review> currentReviewList = GetCurrentReviewListBySubject();

            if (FormManagerData.CurrentReviewIndex >= 0 && FormManagerData.CurrentReviewIndex < currentReviewList.Count)
            {
                return currentReviewList[FormManagerData.CurrentReviewIndex].Text;
            }

            return "";
        }

        // Increments or decrements current review list, and updates the form
        public void LoadNextOrPreviousReview(bool next, Form form)
        {
            if (next)
            {
                // Check if incremented index would exceed list count limit
                if (++FormManagerData.CurrentReviewIndex >= GetCurrentReviewListBySubject().Count)
                {
                    FormManagerData.CurrentReviewIndex--;
                    MessageBox.Show("No more reviews to show!");
                    return;
                }
            }
            else
            {
                if (--FormManagerData.CurrentReviewIndex < 0)
                {
                    FormManagerData.CurrentReviewIndex++;
                    MessageBox.Show("No more reviews to show!");
                    return;
                }
            }

            ChangeForm(form, GetForm(FormType.FORM_READ_REVIEW));
        }

        // Returns fetched reviews for either universities or faculties
        public List<Review> GetCurrentReviewListBySubject()
        {
            switch (FormManagerData.CurrentReviewSubject)
            {
                case ReviewType.REVIEW_UNIVERSITY:
                    return FormManagerData.FoundUniversityReviews;

                case ReviewType.REVIEW_FACULTY:
                    return FormManagerData.FoundFacultyReviews;

                default:
                    return null;
            }
        }

        public void ResetSelectedFaculty()
        {
            FormManagerData.SelectedFaculty = null;
            FormManagerData.FoundFacultyReviews = null;
        }
    }
}