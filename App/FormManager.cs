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
        static int selectedUniversity = -1;
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

        internal static async Task SubmitReview(string text, int value, Form reviewForm)
        {
            Review review = new Review()
            {
                UserId = currentUserGuid,
                Text = text,
                Value = value.ToString()
            };
            if(facultyIndex != -1) // this part only works when we choose from University and Faculty review
            {
                review.FacultyGuid = foundFaculties[facultyIndex].FacultyGuid;
            }
            else
            {
                review.UniGuid = foundUniversities[selectedUniversity].Guid;
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
            if(selectedFaculty != -1)
            {
                facultyIndex = selectedFaculty;
            }
            else
            {
                reviewingUniversity = true;
            }
            ChangeForm(form, GetForm("writeReview"));
        }

        // Returns Name of whatever is reviewed
        internal static string GetNameOfReviewee()
        {
            return reviewingUniversity ? foundUniversities[selectedUniversity].Name : foundFaculties[facultyIndex].Name; // Will need not only faculty or uni
        }

        internal static void SignUpClicked(Form form)
        {
            ChangeForm(form, GetForm("SignUp"));
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

        internal static void SuccessfulSignup(Form form)
        {
            ChangeForm(form, GetForm("loginNoMessage"));
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

        internal static string GetReviewNumber()
        {
            if (reviewLoaded)
            {
                if (currentReviewIndex >= 0 && currentReviewIndex < reviews.Count)
                {
                    return reviews[currentReviewIndex].Value;
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
                var result = await dataManipulations.GetDataFromServer($"review/reviewsByGuid/{foundUniversities[selectedUniversity].Guid}/{(int)GuidType.UNIVERSITY_GUID}");
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

            ChangeForm(form, GetForm("readReview"));
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

            ChangeForm(form, GetForm("readReview"));
        }


        /// <summary>
        /// Returns list of universities that match the search phrase or null if there are no
        /// </summary>
        /// <param name="name">University name</param>
        /// <returns></returns>
        public static async Task<List<string>> GetUniversity(string name)
        {
            List<University> result = new List<University>();
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

        // Checks login details with db and opens application on successful login or relaunches login form
        internal static async Task CheckCredentials(string email, string password, Form form)
        {
            var result = await dataManipulations.GetDataFromServer($"account/login/{email}/{password}");
            currentUserGuid = result;

            if (String.IsNullOrEmpty(result))
            {
                ChangeForm(form, GetForm("login"));
            }
            else
            {
                ChangeForm(form, GetForm("universities"));
            }
        }

        // Opens form for selected University
        internal static void OpenSelected(int selectedIndex, Form form)
        {
            selectedUniversity = selectedIndex;
            ChangeForm(form, GetForm("university"));
        }

        // Closes {form} and opens form {changeTo}
        public static void ChangeForm(Form form, Form changeTo)
        {
            form.Hide();
            changeTo.Closed += (s, args) => form.Close();
            changeTo.Show();
        }

        // Returns new form of selected name or null if doesnt match
        public static Form GetForm(string name)
        {
            switch (name)
            {
                case "universities":
                    return new UniversitySearchForm();
                case "login":
                    return new LoginForm(true);
                case "university":
                    return new SelectedUniversityForm();
                case "writeReview":
                    return new ReviewForm();
                case "readReview":
                    return new ReadReviewForm();
                case "SignUp":
                    return new SignUpForm();
                case "loginNoMessage":
                    return new LoginForm(false);
                default:
                    return null;
            }
        }

        // returns List of faculties names to display/Saves faculties for later use
        public static async Task<List<string>> GetFaculties()
        {
            if (selectedUniversity != -1)
            {
                var data = await dataManipulations.GetDataFromServer($"faculty/{foundUniversities[selectedUniversity].Guid}");
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
