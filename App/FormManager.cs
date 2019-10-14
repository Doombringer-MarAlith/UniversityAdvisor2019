using System;
using System.Collections.Generic;
using Models.Models;
using Newtonsoft.Json;
using ServerCallFromApp;
using System.Windows.Forms;
using System.Linq;

namespace Objektinis
{
    public static class FormManager
    {

        static List<University> foundUnis;
        static int selected = -1;
        static List<Faculty> foundFaculties;
        static int currentReviewIndex = 0;
        static List<Review> reviews;

        internal static string GetNameOfReviewee()
        {
            return "NAME"; // FIX THIS
        }

        // returns text of current review or empty string if the boundaries are reached
        internal static string GetReviewText()
        {
            if(currentReviewIndex > 0 && currentReviewIndex < reviews.Count)
            {
                return reviews[currentReviewIndex].Text;
            }
            else
            {
                return "";
            }
        }

        internal static void LoadReviewsOf(int index, Form form)
        {
            if (index == -1)
            {
                // reviews = GET reviews of selected UNI from db
            }
            else
            {
                // reviews = GET reviews of selected FACULTY from db
            }
            ChangeForm(form, "readReview");
        }

        // loads next review if there is one. Arg true = next, false = previous.
        internal static void LoadReview(bool increment, Form form)
        {
            if (increment)
            {
                if(++currentReviewIndex >= reviews.Count)
                {
                    currentReviewIndex--;
                }
            }
            else
            {
                if(--currentReviewIndex < 0)
                {
                    currentReviewIndex++;
                }
            }
            ChangeForm(form, "readReview");
        }



        // returns list of universities that match the search phrase or null if there are no
        public static List<string> GetUniversity(string name)
        {
            List<University> result = new List<University>();
            string data = DataManipulations.GetDataFromServer($"university/{name}");
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

        internal static void CheckCredentials(string username, string password, Form form)
        {
            var returnGuid = DataManipulations.GetDataFromServer($"account/login/{username}/{password}");
            if(returnGuid == null)
            {
                ChangeForm(form, "login");
            }
            else
            {
                ChangeForm(form, "universities");
            }
            
        }

        // Opens form for selected University
        internal static void OpenSelected(int selectedIndex, Form form)
        {
            selected = selectedIndex;
            ChangeForm(form, "university");
        }

        // Closes {form} and opens form which has name {formName}
        public static void ChangeForm(Form form, string formName)
        {
            Form changeTo = GetForm(formName);
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
                    form = new UniversitiesSearchForm();
                    break;
                case "login":
                    form = new login(true);
                    break;
                case "university":
                    form = new SelectedUniversity();
                    break;
                case "writeReview":
                    form = new reviewForm();
                    break;
                case "readReview":
                    form = new readReviewForm();
                    break;
                default:
                    form = null;
                    break;
            }
            return form;
        }

        public static List<string> GetFaculties()
        {
            if(selected != -1)
            {
                var data = DataManipulations.GetDataFromServer($"faculty/{foundUnis[selected].Guid}");
                if(data != null)
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
