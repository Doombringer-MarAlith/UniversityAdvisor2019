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

        static int selectedUniversityIndex = -1;

        static int facultyIndex = -1;
        static List<Faculty> foundFaculties;
        static int currentReviewIndex = 0;
        static List<Review> reviews;
        static bool reviewingUniversity = true;
        static bool reviewLoaded = true;
        static string currentUserGuid = null;

        enum GuidType
        {
            UNIVERSITY_GUID,
            FACULTY_GUID,
            LECTURER_GUID,
            UNIVERSITY_PROGRAMME_GUID
        }

        enum CreateUserReturn
        {
            EMAIL_TAKEN = 0,
            USERNAME_TAKEN,
            SUCCESS
        }

        public enum FormType
        {
            FORM_LOGIN = 0,
            FORM_SIGN_UP,
            FORM_UNIVERSITIES,
            FORM_SELECTED_UNIVERSITY,
            FORM_REVIEW,
            FORM_READ_REVIEW
        }

        internal static async Task SubmitReview(string text, int value, Form reviewForm)
        {
            Review review = new Review()
            {
                UserId = currentUserGuid,
                Text = text,
                Value = value.ToString()
            };

            if (facultyIndex != -1) // this part only works when we choose from University and Faculty review
            {
                review.FacultyGuid = foundFaculties[facultyIndex].FacultyGuid;
            }
            else
            {
                review.UniGuid = foundUniversities[selectedUniversityIndex].Guid;
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

        internal static void WriteReview(int selectedFaculty, Form form)
        {
            if (selectedFaculty != -1)
            {
                facultyIndex = selectedFaculty;
            }
            else
            {
                reviewingUniversity = true;
            }

            //ChangeForm(form, GetForm(FormType.));
        }

        // Returns Name of whatever is reviewed
        internal static string GetNameOfReviewee()
        {
            return reviewingUniversity ? foundUniversities[selectedUniversityIndex].Name : foundFaculties[facultyIndex].Name; // Will need not only faculty or uni
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

        // Returns text of current review or empty string if the boundaries are reached
        internal static string GetReviewText()
        {
            if (reviewLoaded)
            {
                if (currentReviewIndex >= 0 && currentReviewIndex < reviews.Count)
                {
                    return reviews[currentReviewIndex].Text;
                }
            }

            return "";
        }

        /// <summary>
        /// Loads reviews of what is selected
        /// </summary>
        internal static async Task LoadReviewsOf(int index, Form form)
        {
            if (index == -1)
            {
                // reviews = GET reviews of selected UNI from db
                var result = await dataManipulations.GetDataFromServer($"review/reviewsByGuid/{foundUniversities[selectedUniversityIndex].Guid}/{(int)GuidType.UNIVERSITY_GUID}");
                if (result != null)
                {
                    reviews = JsonConvert.DeserializeObject<List<Review>>(result);
                }
                else
                {
                    reviewLoaded = false;
                }
            }
            else
            {
                // reviews = GET reviews of selected FACULTY from db
                var result = await dataManipulations.GetDataFromServer($"review/reviewsByGuid/{foundFaculties[index].FacultyGuid}/{(int)GuidType.FACULTY_GUID}");
                if (result != null)
                {
                    reviews = JsonConvert.DeserializeObject<List<Review>>(result);
                }
                else
                {
                    reviewLoaded = false;
                }

                facultyIndex = index;
                reviewingUniversity = false;
            }

            ChangeForm(form, GetForm(FormType.FORM_READ_REVIEW));
        }

        // Loads next review if there is one. Arg true = next, false = previous.
        internal static void LoadReview(bool increment, Form form)
        {
            if (increment)
            {
                if (++currentReviewIndex >= reviews.Count)
                {
                    currentReviewIndex--;
                }
            }
            else
            {
                if (--currentReviewIndex < 0)
                {
                    currentReviewIndex++;
                }
            }

            ChangeForm(form, GetForm(FormType.FORM_READ_REVIEW));
        }


        /// <summary>
        /// Returns list of universities that match the search phrase or null if there are none
        /// </summary>
        /// <param name="name">University name</param>
        /// <returns></returns>
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

        // Checks login details with database and opens application on successful login
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

        // Opens form for selected University
        internal static void OpenSelectedUniversity(int selectedIndex, Form form)
        {
            selectedUniversityIndex = selectedIndex;
            ChangeForm(form, GetForm(FormType.FORM_SELECTED_UNIVERSITY));
        }

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
                case FormType.FORM_REVIEW:
                    return new ReviewForm();
                case FormType.FORM_READ_REVIEW:
                    return new ReadReviewForm();
                default:
                    return null;
            }
        }

        // returns List of faculties names to display/Saves faculties for later use
        public static async Task<List<string>> GetFaculties()
        {
            if (selectedUniversityIndex != -1)
            {
                var data = await dataManipulations.GetDataFromServer($"faculty/{foundUniversities[selectedUniversityIndex].Guid}");
                if (data != null)
                {
                    foundFaculties = JsonConvert.DeserializeObject<List<Faculty>>(data);
                    return foundFaculties.Select(fac => fac.Name).ToList();
                }
            }

            return null;
        }

        //what type this is supposed to be?
        /* public static List<Review> GetUniReview(string name)
         {

         }*/

    }
}
