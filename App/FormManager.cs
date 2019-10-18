using System.Collections.Generic;
using Models.Models;
using Newtonsoft.Json;
using ServerCallFromApp;
using System.Windows.Forms;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;



namespace App
{
    public static class FormManager
    {

        static List<University> foundUnis;
        static int selectedUni = -1;
        static int facultyIndex = -1;
        static List<Faculty> foundFaculties;
        static int currentReviewIndex = 0;
        static List<Review> reviews;
        static bool reviewingUni = true;
        static readonly DataManipulations dataManipulations = new DataManipulations(new HttpClient());
        static bool reviewLoaded = true;
        static string currentUserGuid = null;

        enum GuidType
        {
            UniGuid,
            FacultyGuid,
            LecturerGuid,
            UniProgramGuid
        }

        // Returns Name of whatever is reviewed
        internal static string GetNameOfReviewee()
        {
            return reviewingUni ? foundUnis[selectedUni].Name : foundFaculties[facultyIndex].Name; // FIX THIS
        }

        // returns text of current review or empty string if the boundaries are reached
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
                var res = await dataManipulations.GetDataFromServer($"review/reviewsByGuid/{foundUnis[selectedUni].Guid}/{(int)GuidType.UniGuid}");
                if (res != null)
                {
                    reviews = JsonConvert.DeserializeObject<List<Review>>(res);
                }
                else
                {
                    reviewLoaded = false;
                }
            }
            else
            {
                // reviews = GET reviews of selected FACULTY from db
                var res = await dataManipulations.GetDataFromServer($"review/reviewsByGuid/{foundFaculties[index].FacultyGuid}/{(int)GuidType.FacultyGuid}");
                if (res != null)
                {
                    reviews = JsonConvert.DeserializeObject<List<Review>>(res);
                }
                else
                {
                    reviewLoaded = false;
                }
                facultyIndex = index;
                reviewingUni = false;
            }
            ChangeForm(form, GetForm("readReview"));
        }

        // loads next review if there is one. Arg true = next, false = previous.
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
                foundUnis = JsonConvert.DeserializeObject<List<University>>(data);
            }
            else
            {
                return null;
            }
            return foundUnis.Select(uni => uni.Name).ToList();
        }

        // checks login details with db and opens application on successful login or relaunches login form
        internal static async Task CheckCredentials(string username, string password, Form form)
        {
            currentUserGuid = await dataManipulations.GetDataFromServer($"account/login/{username}/{password}");
            if (currentUserGuid == null)
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
            selectedUni = selectedIndex;
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
            Form form;
            switch (name)
            {
                case "universities":
                    form = new UniversitySearchForm();
                    break;
                case "login":
                    form = new LoginForm(true);
                    break;
                case "university":
                    form = new SelectedUniversityForm();
                    break;
                case "writeReview":
                    form = new ReviewForm();
                    break;
                case "readReview":
                    form = new ReadReviewForm();
                    break;
                default:
                    form = null;
                    break;
            }
            return form;
        }

        // returns List of faculties names to display/Saves faculties for later use
        public static async Task<List<string>> GetFaculties()
        {
            if (selectedUni != -1)
            {
                var data = await dataManipulations.GetDataFromServer($"faculty/{foundUnis[selectedUni].Guid}");
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
