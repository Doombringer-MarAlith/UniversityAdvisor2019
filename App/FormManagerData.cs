using App;
using Models.Models;
using System.Collections.Generic;

namespace Objektinis
{
    public class FormManagerData
    {
        internal List<University> FoundUniversities;
        internal List<Faculty> FoundFaculties;
        internal List<Review> FoundUniversityReviews;
        internal List<Review> FoundFacultyReviews;

        internal ReviewType CurrentReviewSubject = ReviewType.REVIEW_NONE;
        internal int CurrentReviewIndex = 0;

        internal University SelectedUniversity;
        internal Faculty SelectedFaculty;

        internal string CurrentUserGuid;
    }
}