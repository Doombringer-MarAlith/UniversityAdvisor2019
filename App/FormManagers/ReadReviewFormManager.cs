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
    public abstract class ReadReviewFormManager : BaseFormManager
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

        protected ReadReviewFormManager(IDataManipulations dataManipulations) : base(dataManipulations)
        {
            _dataManipulations = dataManipulations;
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

        internal  void ResetSelectedFaculty()
        {
            _selectedFaculty = null;
            _foundFacultyReviews = null;
        }
    }
}