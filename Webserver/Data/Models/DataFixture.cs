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
                    FoundingDate = DateTime.Now,
                    Id = "1"
                });

                dbContext.Universities.Add(new University
                {
                    Name = "KTU",
                    Description = "Decent",
                    Location = "Unknown",
                    FoundingDate = DateTime.Now,
                    Id = "2"
                });

                dbContext.Faculties.Add(new Faculty
                {
                    Name = "MIF",
                    UniversityId = "1",
                    Id = "10"
                });

                dbContext.Faculties.Add(new Faculty
                {
                    Name = "FIM",
                    UniversityId = "1",
                    Id = "11"
                });

                dbContext.Faculties.Add(new Faculty
                {
                    Name = "KTU Faculty #1",
                    UniversityId = "2",
                    Id = "12"
                });

                dbContext.Faculties.Add(new Faculty
                {
                    Name = "KTU Faculty #2",
                    UniversityId = "2",
                    Id = "13"
                });

                dbContext.Reviews.Add(new Review
                {
                    Text = "Perfect Uni!",
                    UniversityId = "1",
                    Id = "100"
                });

                dbContext.Reviews.Add(new Review
                {
                    Text = "Perfect KTUni!",
                    UniversityId = "2",
                    Id = "101"
                });

                dbContext.Reviews.Add(new Review
                {
                    Text = "Perfect Faculty!",
                    FacultyId = "10",
                    Id = "102"
                });

                dbContext.Reviews.Add(new Review
                {
                    Text = "Perfect VU Faculty!",
                    FacultyId = "11",
                    Id = "103"
                });

                dbContext.Reviews.Add(new Review
                {
                    Text = "Nice!",
                    FacultyId = "12",
                    Id = "104"
                });

                dbContext.Reviews.Add(new Review
                {
                    Text = "Legit!",
                    FacultyId = "13",
                    Id = "105"
                });
            }

            dbContext.SaveChanges();
        }
    }
}