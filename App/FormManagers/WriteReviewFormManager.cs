using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Models.Models;
using Newtonsoft.Json;
using ServerCallFromApp;

namespace App.FormManagers
{
    public class WriteReviewFormManager : BaseFormManager, IWriteReviewFormManager
    {
        public WriteReviewFormManager(IDataManipulations dataManipulations, FormManagerData formManagerData) : base(dataManipulations, formManagerData)
        {
        }

        public async Task SubmitReview(string text, int value, Form reviewForm)
        {
            Review review = new Review()
            {
                UserId = FormManagerData.CurrentUserGuid,
                Text = text,
                Value = value.ToString()
            };

            if (FormManagerData.CurrentReviewSubject == ReviewType.REVIEW_FACULTY)
            {
                review.FacultyGuid = FormManagerData.SelectedFaculty.FacultyGuid;
            }
            else if (FormManagerData.CurrentReviewSubject == ReviewType.REVIEW_UNIVERSITY)
            {
                review.UniGuid = FormManagerData.SelectedUniversity.Guid;
            }

            string data;
            do
            {
                review.ReviewGuid = Guid.NewGuid().ToString();
                data = await DataManipulations.GetDataFromServer($"review/{review.ReviewGuid}");
            }
            while (!String.IsNullOrEmpty(data));

            await DataManipulations.PostDataToServer($"review/create", JsonConvert.SerializeObject(review));
        }

        public void ResetSelectedFaculty()
        {
            FormManagerData.SelectedFaculty = null;
            FormManagerData.FoundFacultyReviews = null;
        }
    }
}