﻿using Models.Models;
using Newtonsoft.Json;
using Objektinis;
using ServerCallFromApp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public class SelectedUniversityFormManager : BaseFormManager, ISelectedUniversityFormManager
    {
        protected SelectedUniversityFormManager(IDataManipulations dataManipulations, FormManagerData formManagerData) : base(dataManipulations, formManagerData)
        {
        }

        public void OpenWriteReviewFormForSelectedFaculty(int selectedFacultyIndex, Form form)
        {
            FormManagerData.SelectedFaculty = FormManagerData.FoundFaculties[selectedFacultyIndex];
            FormManagerData.CurrentReviewSubject = ReviewType.REVIEW_FACULTY;
            ChangeForm(form, GetForm(FormType.FORM_WRITE_REVIEW));
        }

        public void OpenWriteReviewFormForSelectedUniversity(Form form)
        {
            FormManagerData.CurrentReviewSubject = ReviewType.REVIEW_UNIVERSITY;
            ChangeForm(form, GetForm(FormType.FORM_WRITE_REVIEW));
        }

        // Loads reviews of current selected university and opens review reading form
        public async Task LoadReviewsForSelectedUniversity(Form form)
        {
            FormManagerData.CurrentReviewSubject = ReviewType.REVIEW_UNIVERSITY;

            if (FormManagerData.SelectedUniversity != null)
            {
                var result = await DataManipulations.GetDataFromServer($"review/reviewsByGuid/{FormManagerData.SelectedUniversity.Guid}/{(int)GuidType.UNIVERSITY_GUID}");
                if (result != null)
                {
                    FormManagerData.FoundUniversityReviews = JsonConvert.DeserializeObject<List<Review>>(result);
                }
            }

            ChangeForm(form, GetForm(FormType.FORM_READ_REVIEW));
        }

        // Loads reviews of current selected faculty and opens review reading form
        public async Task LoadReviewsForSelectedFaculty(int selectedFacultyIndex, Form form)
        {
            FormManagerData.SelectedFaculty = FormManagerData.FoundFaculties[selectedFacultyIndex];
            FormManagerData.CurrentReviewSubject = ReviewType.REVIEW_FACULTY;

            if (FormManagerData.SelectedFaculty != null)
            {
                var result = await DataManipulations.GetDataFromServer($"review/reviewsByGuid/{FormManagerData.SelectedFaculty.FacultyGuid}/{(int)GuidType.FACULTY_GUID}");
                if (result != null)
                {
                    FormManagerData.FoundFacultyReviews = JsonConvert.DeserializeObject<List<Review>>(result);
                }
            }

            ChangeForm(form, GetForm(FormType.FORM_READ_REVIEW));
        }

        // Returns a list of faculty names to display and saves faculties for later use
        public async Task<List<string>> GetFaculties()
        {
            if (FormManagerData.SelectedUniversity != null)
            {
                var data = await DataManipulations.GetDataFromServer($"faculty/{FormManagerData.SelectedUniversity.Guid}");
                if (data != null)
                {
                    FormManagerData.FoundFaculties = JsonConvert.DeserializeObject<List<Faculty>>(data);
                    return FormManagerData.FoundFaculties.Select(fac => fac.Name).ToList();
                }
            }

            return null;
        }

        public string GetSelectedUniversityName()
        {
            return FormManagerData.SelectedUniversity.Name;
        }

        public void CloseSelectedUniversityForm(Form form)
        {
            ResetSelectedFaculty();
            ResetSelectedUniversity();

            ChangeForm(form, GetForm(FormType.FORM_UNIVERSITIES));
        }

        public void ResetSelectedUniversity()
        {
            FormManagerData.SelectedUniversity = null;
            FormManagerData.FoundUniversityReviews = null;
        }

        public void ResetSelectedFaculty()
        {
            FormManagerData.SelectedFaculty = null;
            FormManagerData.FoundFacultyReviews = null;
        }
    }
}