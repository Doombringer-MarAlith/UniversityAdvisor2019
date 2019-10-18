﻿using System.Collections.Generic;
using Models.Models;
using Newtonsoft.Json;
using ServerCallFromApp;
using System.Windows.Forms;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System;

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
            return reviewingUni ? foundUnis[selectedUni].Name : foundFaculties[facultyIndex].Name; // Will need not only faculty or uni
        }

        internal static void SignUpClicked(Form form)
        {
            ChangeForm(form, GetForm("SignUp"));
        }

        internal static async Task<int> CreateUser(string username, string email, string password)
        {
            // check for existing email : return 0 if there is
            Account account = new Account()
            {
                Name = username,
                Email = email,
                Password = password
            };
            var data = await dataManipulations.GetDataFromServer($"account/checkByEmail/{JsonConvert.SerializeObject(email)}/{true}");
            if(!String.IsNullOrEmpty(data))
            {
                return 0;
            }

            // check for existing username : return 1
            data = await dataManipulations.GetDataFromServer($"account/checkByUsername/{JsonConvert.SerializeObject(username)}/{true}");
            if(!String.IsNullOrEmpty(data))
            {
                return 1;
            }

            // check existing guid
            do
            {
                account.Guid = GenerateRandomString(50);
                data = await dataManipulations.GetDataFromServer($"account/{account.Guid}");
            }
            while (String.IsNullOrEmpty(data));

            // create Account
            await dataManipulations.PostDataToServer("account/create", JsonConvert.SerializeObject(account));
            return 2; // success
        }

        internal static void SuccessfulSignup(Form form)
        {
            ChangeForm(form, GetForm("loginNoMessage"));
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
                case "SignUp":
                    form = new SignUpForm();
                    break;
                case "loginNoMessage":
                    form = new LoginForm(false);
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

        public static string GenerateRandomString(int length)
        {
            Random random = new Random();
            string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }

            return result.ToString();
        }
    }
}
