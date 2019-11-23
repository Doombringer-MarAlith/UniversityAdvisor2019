using Models;
using System;
using System.Linq;

namespace Webserver.Data.Models
{
    public static class DataFixture
    {
        public static void Initialize(ApplicationDbContext dbContext)
        {
            if (!dbContext.Universities.Any())
            {
                dbContext.Universities.Add(new University
                {
                    Name = "Vilnius University",
                    Description = "Decent",
                    Location = "Unknown",
                    FoundingDate = DateTime.Now
                });

                dbContext.Universities.Add(new University
                {
                    Name = "KTU",
                    Description = "Decent",
                    Location = "Unknown",
                    FoundingDate = DateTime.Now
                });

                dbContext.Faculties.Add(new Faculty
                {
                    Name = "MIF",
                    UniversityId = 1
                });

                dbContext.Faculties.Add(new Faculty
                {
                    Name = "FIM",
                    UniversityId = 1
                });

                dbContext.Faculties.Add(new Faculty
                {
                    Name = "KTU Faculty #1",
                    UniversityId = 2
                });

                dbContext.Faculties.Add(new Faculty
                {
                    Name = "KTU Faculty #2",
                    UniversityId = 2
                });

                dbContext.Reviews.Add(new Review
                {
                    Text = "Perfect Uni!",
                    UniversityId = 1,
                    UserId = "1",
                    Value = 5
                });

                dbContext.Reviews.Add(new Review
                {
                    Text = "Perfect KTUni!",
                    UniversityId = 2,
                    UserId = "1",
                    Value = 5
                });

                dbContext.Reviews.Add(new Review
                {
                    Text = "Perfect Faculty!",
                    FacultyId = 1,
                    UserId = "1",
                    Value = 5
                });

                dbContext.Reviews.Add(new Review
                {
                    Text = "Perfect VU Faculty!",
                    FacultyId = 2,
                    UserId = "1",
                    Value = 5
                });

                dbContext.Reviews.Add(new Review
                {
                    Text = "Nice!",
                    FacultyId = 3,
                    UserId = "1",
                    Value = 5
                });

                dbContext.Reviews.Add(new Review
                {
                    Text = "Legit!",
                    FacultyId = 4,
                    UserId = "1",
                    Value = 5
                });

                dbContext.Programmes.Add(new Programme
                {
                    Name = "Programų sistemos",
                    FacultyId = 4,
                    UniversityId = 2
                });

                dbContext.SaveChanges();
            }
        }
    }
}