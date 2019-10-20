using System.Collections.Generic;
using Models.Models;
using Newtonsoft.Json;
using ServerCallFromApp;
using System.Windows.Forms;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using App.Helpers;

namespace App
{
    public static class FormManager
    {
        static readonly DataManipulations dataManipulations = new DataManipulations(new HttpClient());
        static List<University> foundUniversities;
        static List<Faculty> foundFaculties;
        static List<Review> foundUniversityReviews;
        static List<Review> foundFacultyReviews;

        static ReviewType currentReviewSubject = ReviewType.REVIEW_NONE;
        static int currentReviewIndex = 0;

        static University selectedUniversity;
        static Faculty selectedFaculty;

        static string currentUserGuid = null;

        internal static async Task TryToLogIn(string email, string password, Form loginForm)
        {
            var result = await dataManipulations.GetDataFromServer($"account/login/{email}/{password}");
            if (String.IsNullOrEmpty(result))
            {
                MessageBox.Show("Account with such credentials does not exist!");
            }
            else
            {
                currentUserGuid = result;
                ChangeForm(loginForm, GetForm(FormType.FORM_UNIVERSITIES));
            }
        }

        internal static async Task<int> CreateUser(string username, string email, string password)
        {
            // Check for existing email
            var data = await dataManipulations.GetDataFromServer($"account/checkByEmail/{email}/{true}");
            if (!String.IsNullOrEmpty(data))
            {
                return (int)CreateUserReturn.EMAIL_TAKEN;
            }

            // Check for existing username
            data = await dataManipulations.GetDataFromServer($"account/checkByUsername/{username}/{0}");
            if (!String.IsNullOrEmpty(data))
            {
                return (int)CreateUserReturn.USERNAME_TAKEN;
            }

            // Check for existing guid
            Account account = new Account()
            {
                Name = username,
                Email = email,
                Password = password
            };

            do
            {
                account.Guid = Helper.GenerateRandomString(50);
                data = await dataManipulations.GetDataFromServer($"account/{account.Guid}");
            }
            while (String.IsNullOrEmpty(data));

            // Create an account
            await dataManipulations.PostDataToServer("account/create", JsonConvert.SerializeObject(account));
            return (int)CreateUserReturn.SUCCESS;
        }

        internal static async Task SubmitReview(string text, int value, Form reviewForm)
        {
            Review review = new Review()
            {
                UserId = currentUserGuid,
                Text = text,
                Value = value.ToString()
            };

            if (currentReviewSubject == ReviewType.REVIEW_FACULTY)
            {
                review.FacultyGuid = selectedFaculty.FacultyGuid;
            }
            else if (currentReviewSubject == ReviewType.REVIEW_UNIVERSITY)
            {
                review.UniGuid = selectedUniversity.Guid;
            }

            var data = "something";
            do
            {
                review.ReviewGuid = Helper.GenerateRandomString(50);
                data = await dataManipulations.GetDataFromServer($"review/{review.ReviewGuid}");
            }
            while (String.IsNullOrEmpty(data));

            await dataManipulations.PostDataToServer($"review/create", JsonConvert.SerializeObject(review));
        }

        internal static void OpenWriteReviewFormForSelectedFaculty(int selectedFacultyIndex, Form form)
        {
            selectedFaculty = foundFaculties[selectedFacultyIndex];
            currentReviewSubject = ReviewType.REVIEW_FACULTY;
            ChangeForm(form, GetForm(FormType.FORM_WRITE_REVIEW));
        }

        internal static void OpenWriteReviewFormForSelectedUniversity(Form form)
        {
            currentReviewSubject = ReviewType.REVIEW_UNIVERSITY;
            ChangeForm(form, GetForm(FormType.FORM_WRITE_REVIEW));
        }

        // Returns the name of whatever is currently reviewed
        internal static string GetNameOfReviewee()
        {
            switch (currentReviewSubject)
            {
                case ReviewType.REVIEW_UNIVERSITY:
                    return selectedUniversity.Name;
                case ReviewType.REVIEW_FACULTY:
                    return selectedFaculty.Name;
                default:
                    return "";
            }
        }

        // Returns text of current review or empty string if the boundaries are reached
        internal static string GetReviewText()
        {
            List<Review> currentReviewList = GetCurrentReviewListBySubject();

            if (currentReviewIndex >= 0 && currentReviewIndex < currentReviewList.Count)
            {
                return currentReviewList[currentReviewIndex].Text;
            }

            return "";
        }

        // Loads reviews of current selected university and opens review reading form
        internal static async Task LoadReviewsForSelectedUniversity(Form form)
        {
            currentReviewSubject = ReviewType.REVIEW_UNIVERSITY;

            if (selectedUniversity != null)
            {
                var result = await dataManipulations.GetDataFromServer($"review/reviewsByGuid/{selectedUniversity.Guid}/{(int)GuidType.UNIVERSITY_GUID}");
                if (result != null)
                {
                    foundUniversityReviews = JsonConvert.DeserializeObject<List<Review>>(result);
                }
            }

            ChangeForm(form, GetForm(FormType.FORM_READ_REVIEW));
        }

        // Loads reviews of current selected faculty and opens review reading form
        internal static async Task LoadReviewsForSelectedFaculty(int selectedFacultyIndex, Form form)
        {
            selectedFaculty = foundFaculties[selectedFacultyIndex];
            currentReviewSubject = ReviewType.REVIEW_FACULTY;

            if (selectedFaculty != null)
            {
                var result = await dataManipulations.GetDataFromServer($"review/reviewsByGuid/{selectedFaculty.FacultyGuid}/{(int)GuidType.FACULTY_GUID}");
                if (result != null)
                {
                    foundFacultyReviews = JsonConvert.DeserializeObject<List<Review>>(result);
                }
            }

            ChangeForm(form, GetForm(FormType.FORM_READ_REVIEW));
        }

        // Increments or decrements current review list, and updates the form
        internal static void LoadNextOrPreviousReview(bool next, Form form)
        {
            if (next)
            {
                // Check if incremented index would exceed list count limit
                if (++currentReviewIndex >= GetCurrentReviewListBySubject().Count)
                {
                    currentReviewIndex--;
                    MessageBox.Show("No more reviews to show!");
                    return;
                }
            }
            else
            {
                if (--currentReviewIndex < 0)
                {
                    currentReviewIndex++;
                    MessageBox.Show("No more reviews to show!");
                    return;
                }
            }

            ChangeForm(form, GetForm(FormType.FORM_READ_REVIEW));
        }

        // Returns fetched reviews for either universities or faculties
        internal static List<Review> GetCurrentReviewListBySubject()
        {
            switch (currentReviewSubject)
            {
                case ReviewType.REVIEW_UNIVERSITY:
                    return foundUniversityReviews;
                case ReviewType.REVIEW_FACULTY:
                    return foundFacultyReviews;
                default:
                    return null;
            }
        }

        // Returns a list of universities that match the search phrase or null if there are none
        public static async Task<List<string>> GetUniversities(string name)
        {
            string data = await dataManipulations.GetDataFromServer($"university/{name}");
            if (data != null)
            {
                foundUniversities = JsonConvert.DeserializeObject<List<University>>(data);
            }
            else
            {
                return null;
            }

            return foundUniversities.Select(uni => uni.Name).ToList();
        }

        // Returns a list of faculty names to display and saves faculties for later use
        public static async Task<List<string>> GetFaculties()
        {
            if (selectedUniversity != null)
            {
                var data = await dataManipulations.GetDataFromServer($"faculty/{selectedUniversity.Guid}");
                if (data != null)
                {
                    foundFaculties = JsonConvert.DeserializeObject<List<Faculty>>(data);
                    return foundFaculties.Select(fac => fac.Name).ToList();
                }
            }

            return null;
        }

        public static string GetSelectedUniversityName()
        {
            return selectedUniversity.Name;
        }

        //what type this is supposed to be?
        /* public static List<Review> GetUniReview(string name)
         {

         }*/
        
        public static void ChangeForm(Form form, Form changeTo)
        {
            form.Hide();
            changeTo.Closed += (s, args) => form.Close();
            changeTo.Show();
        }

        public static Form GetForm(FormType formType)
        {
            switch (formType)
            {
                case FormType.FORM_LOGIN:
                    return new LoginForm();
                case FormType.FORM_SIGN_UP:
                    return new SignUpForm();
                case FormType.FORM_UNIVERSITIES:
                    return new UniversitySearchForm();
                case FormType.FORM_SELECTED_UNIVERSITY:
                    return new SelectedUniversityForm();
                case FormType.FORM_WRITE_REVIEW:
                    return new WriteReviewForm();
                case FormType.FORM_READ_REVIEW:
                    return new ReadReviewForm();
                default:
                    return null;
            }
        }

        internal static void OpenSelectedUniversityForm(int selectedUniversityIndex, Form form)
        {
            selectedUniversity = foundUniversities[selectedUniversityIndex];
            ChangeForm(form, GetForm(FormType.FORM_SELECTED_UNIVERSITY));
        }

        internal static void CloseSelectedUniversityForm(Form form)
        {
            ResetSelectedFaculty();
            ResetSelectedUniversity();

            ChangeForm(form, GetForm(FormType.FORM_UNIVERSITIES));
        }

        internal static void ResetSelectedUniversity()
        {
            selectedUniversity = null;
            foundUniversityReviews = null;
        }

        internal static void ResetSelectedFaculty()
        {
            selectedFaculty = null;
            foundFacultyReviews = null;
        }
    }

    public enum FormType
    {
        FORM_LOGIN,
        FORM_SIGN_UP,
        FORM_UNIVERSITIES,
        FORM_SELECTED_UNIVERSITY,
        FORM_WRITE_REVIEW,
        FORM_READ_REVIEW
    }

    enum GuidType
    {
        UNIVERSITY_GUID,
        FACULTY_GUID,
        LECTURER_GUID,
        UNIVERSITY_PROGRAMME_GUID
    }

    enum CreateUserReturn
    {
        EMAIL_TAKEN,
        USERNAME_TAKEN,
        SUCCESS
    }

    enum ReviewType
    {
        REVIEW_UNIVERSITY,
        REVIEW_FACULTY,
        REVIEW_NONE
    }
}
