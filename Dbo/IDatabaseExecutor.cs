using System.Collections.Generic;
using Models.Models;

namespace Dbo
{
    public interface IDatabaseExecutor
    {
        void CreateAccount(Account account);
        string CheckAccountEmail(string email);
        string CheckAccountUsername(string username);
        Account ReturnAccount(string id);
        string ReturnAccountGuid(string email, string password);
        void DeleteAccount(string id);
        object ReturnReviews(string Guid, int guidType);
        void CreateUniversity(University university);
        List<University> ReturnUniversities(string name);
        void CreateFaculty(Faculty faculty);
        List<Faculty> ReturnFaculties(string guid);
        object ReturnReview(string Guid);
        void CreateReview(Review review);
    }
}