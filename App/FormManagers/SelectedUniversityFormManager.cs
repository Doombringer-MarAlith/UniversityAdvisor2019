using App.Helpers;
using ExternalDependencies;
using Models.Models;
using Newtonsoft.Json;
using ServerCallFromApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public abstract class SelectedUniversityFormManager : BaseFormManager
    {
        private readonly IDataManipulations _dataManipulations;
        private  List<University> _foundUniversities;
        private  List<Faculty> _foundFaculties;
        private  List<Review> _foundUniversityReviews;
        private  List<Review> _foundFacultyReviews;

        private  ReviewType _currentReviewSubject = ReviewType.REVIEW_NONE;
        private  int _currentReviewIndex = 0;

        private  University _selectedUniversity;
        private  Faculty _selectedFaculty;

        private  string _currentUserGuid;

        protected SelectedUniversityFormManager(IDataManipulations dataManipulations) : base(dataManipulations)
        {
            _dataManipulations = dataManipulations;
        }
        internal async Task TryToLogIn(string email, string password, Form loginForm)
        {
            var result = await _dataManipulations.GetDataFromServer($"account/login/{email}/{password}");
            if (String.IsNullOrEmpty(result))
            {
                MessageBox.Show("Account with such credentials does not exist!");
            }
            else
            {
                _currentUserGuid = result;
                ChangeForm(loginForm, GetForm(FormType.FORM_UNIVERSITIES));
            }
        }

        internal  async Task<int> CreateUser(string username, string email, string password)
        {
            // Check for existing email
            var data = await _dataManipulations.GetDataFromServer($"account/checkByEmail/{email}/{true}");
            if (!String.IsNullOrEmpty(data))
            {
                return (int)CreateUserReturn.EMAIL_TAKEN;
            }

            // Check for existing username
            data = await _dataManipulations.GetDataFromServer($"account/checkByUsername/{username}/{0}");
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
                data = await _dataManipulations.GetDataFromServer($"account/{account.Guid}");
            }
            while (String.IsNullOrEmpty(data));

            // Create an account
            await _dataManipulations.PostDataToServer("account/create", JsonConvert.SerializeObject(account));
            return (int)CreateUserReturn.SUCCESS;
        }

        internal  async Task SubmitReview(string text, int value, Form reviewForm)
        {
            Review review = new Review()
            {
                UserId = _currentUserGuid,
                Text = text,
                Value = value.ToString()
            };

            if (_currentReviewSubject == ReviewType.REVIEW_FACULTY)
            {
                review.FacultyGuid = _selectedFaculty.FacultyGuid;
            }
            else if (_currentReviewSubject == ReviewType.REVIEW_UNIVERSITY)
            {
                review.UniGuid = _selectedUniversity.Guid;
            }

            var data = "something";
            do
            {
                review.ReviewGuid = Helper.GenerateRandomString(50);
                data = await _dataManipulations.GetDataFromServer($"review/{review.ReviewGuid}");
            }
            while (String.IsNullOrEmpty(data));

            await _dataManipulations.PostDataToServer($"review/create", JsonConvert.SerializeObject(review));
        }

        internal  void OpenWriteReviewFormForSelectedFaculty(int selectedFacultyIndex, Form form)
        {
            _selectedFaculty = _foundFaculties[selectedFacultyIndex];
            _currentReviewSubject = ReviewType.REVIEW_FACULTY;
            ChangeForm(form, GetForm(FormType.FORM_WRITE_REVIEW));
        }

        internal  void OpenWriteReviewFormForSelectedUniversity(Form form)
        {
            _currentReviewSubject = ReviewType.REVIEW_UNIVERSITY;
            ChangeForm(form, GetForm(FormType.FORM_WRITE_REVIEW));
        }

        // Returns the name of whatever is currently reviewed
        internal  string GetNameOfReviewee()
        {
            switch (_currentReviewSubject)
            {
                case ReviewType.REVIEW_UNIVERSITY:
                    return _selectedUniversity.Name;

                case ReviewType.REVIEW_FACULTY:
                    return _selectedFaculty.Name;

                default:
                    return "";
            }
        }

        // Returns text of current review or empty string if the boundaries are reached
        internal  string GetReviewText()
        {
            List<Review> currentReviewList = GetCurrentReviewListBySubject();

            if (_currentReviewIndex >= 0 && _currentReviewIndex < currentReviewList.Count)
            {
                return currentReviewList[_currentReviewIndex].Text;
            }

            return "";
        }

        // Loads reviews of current selected university and opens review reading form
        internal  async Task LoadReviewsForSelectedUniversity(Form form)
        {
            _currentReviewSubject = ReviewType.REVIEW_UNIVERSITY;

            if (_selectedUniversity != null)
            {
                var result = await _dataManipulations.GetDataFromServer($"review/reviewsByGuid/{_selectedUniversity.Guid}/{(int)GuidType.UNIVERSITY_GUID}");
                if (result != null)
                {
                    _foundUniversityReviews = JsonConvert.DeserializeObject<List<Review>>(result);
                }
            }

            ChangeForm(form, GetForm(FormType.FORM_READ_REVIEW));
        }

        // Loads reviews of current selected faculty and opens review reading form
        internal  async Task LoadReviewsForSelectedFaculty(int selectedFacultyIndex, Form form)
        {
            _selectedFaculty = _foundFaculties[selectedFacultyIndex];
            _currentReviewSubject = ReviewType.REVIEW_FACULTY;

            if (_selectedFaculty != null)
            {
                var result = await _dataManipulations.GetDataFromServer($"review/reviewsByGuid/{_selectedFaculty.FacultyGuid}/{(int)GuidType.FACULTY_GUID}");
                if (result != null)
                {
                    _foundFacultyReviews = JsonConvert.DeserializeObject<List<Review>>(result);
                }
            }

            ChangeForm(form, GetForm(FormType.FORM_READ_REVIEW));
        }

        // Increments or decrements current review list, and updates the form
        internal  void LoadNextOrPreviousReview(bool next, Form form)
        {
            if (next)
            {
                // Check if incremented index would exceed list count limit
                if (++_currentReviewIndex >= GetCurrentReviewListBySubject().Count)
                {
                    _currentReviewIndex--;
                    MessageBox.Show("No more reviews to show!");
                    return;
                }
            }
            else
            {
                if (--_currentReviewIndex < 0)
                {
                    _currentReviewIndex++;
                    MessageBox.Show("No more reviews to show!");
                    return;
                }
            }

            ChangeForm(form, GetForm(FormType.FORM_READ_REVIEW));
        }

        // Returns fetched reviews for either universities or faculties
        internal  List<Review> GetCurrentReviewListBySubject()
        {
            switch (_currentReviewSubject)
            {
                case ReviewType.REVIEW_UNIVERSITY:
                    return _foundUniversityReviews;

                case ReviewType.REVIEW_FACULTY:
                    return _foundFacultyReviews;

                default:
                    return null;
            }
        }

        // Returns a list of universities that match the search phrase or null if there are none
        public  async Task<List<string>> GetUniversities(string name)
        {
            string data = await _dataManipulations.GetDataFromServer($"university/{name}");
            if (data != null)
            {
                _foundUniversities = JsonConvert.DeserializeObject<List<University>>(data);
            }
            else
            {
                return null;
            }

            return _foundUniversities.Select(uni => uni.Name).ToList();
        }

        // Returns a list of faculty names to display and saves faculties for later use
        public  async Task<List<string>> GetFaculties()
        {
            if (_selectedUniversity != null)
            {
                var data = await _dataManipulations.GetDataFromServer($"faculty/{_selectedUniversity.Guid}");
                if (data != null)
                {
                    _foundFaculties = JsonConvert.DeserializeObject<List<Faculty>>(data);
                    return _foundFaculties.Select(fac => fac.Name).ToList();
                }
            }

            return null;
        }

        public  string GetSelectedUniversityName()
        {
            return _selectedUniversity.Name;
        }

        internal  void OpenSelectedUniversityForm(int selectedUniversityIndex, Form form)
        {
            _selectedUniversity = _foundUniversities[selectedUniversityIndex];
            ChangeForm(form, GetForm(FormType.FORM_SELECTED_UNIVERSITY));
        }

        internal  void CloseSelectedUniversityForm(Form form)
        {
            ResetSelectedFaculty();
            ResetSelectedUniversity();

            ChangeForm(form, GetForm(FormType.FORM_UNIVERSITIES));
        }

        internal  void ResetSelectedUniversity()
        {
            _selectedUniversity = null;
            _foundUniversityReviews = null;
        }

        internal  void ResetSelectedFaculty()
        {
            _selectedFaculty = null;
            _foundFacultyReviews = null;
        }
    }
}