﻿using Models.Models;
using Newtonsoft.Json;
using Objektinis;
using ServerCallFromApp;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public abstract class WriteReviewFormManager : BaseFormManager, IWriteReviewFormManager
    {
        protected WriteReviewFormManager(IDataManipulations dataManipulations, FormManagerData formManagerData) : base(dataManipulations, formManagerData)
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

            var data = "something";
            do
            {
                review.ReviewGuid = Guid.NewGuid().ToString();
                data = await DataManipulations.GetDataFromServer($"review/{review.ReviewGuid}");
            }
            while (String.IsNullOrEmpty(data));

            await DataManipulations.PostDataToServer($"review/create", JsonConvert.SerializeObject(review));
        }

        public void ResetSelectedFaculty()
        {
            FormManagerData.SelectedFaculty = null;
            FormManagerData.FoundFacultyReviews = null;
        }
    }
}