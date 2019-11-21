using System;
using System.Linq;
using Webserver.Models;

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
                    UniGuid = "1",
                    Id = "10"
                });

                dbContext.Faculties.Add(new Faculty
                {
                    Name = "FIM",
                    UniGuid = "1",
                    Id = "11"
                });

                dbContext.Faculties.Add(new Faculty
                {
                    Name = "KTU Faculty #1",
                    UniGuid = "2",
                    Id = "12"
                });

                dbContext.Faculties.Add(new Faculty
                {
                    Name = "KTU Faculty #2",
                    UniGuid = "2",
                    Id = "13"
                });

                dbContext.Reviews.Add(new Review
                {
                    Text = "Perfect Uni!",
                    UniGuid = "1",
                    Id = "100"
                });

                dbContext.Reviews.Add(new Review
                {
                    Text = "Perfect KTUni!",
                    UniGuid = "2",
                    Id = "101"
                });

                dbContext.Reviews.Add(new Review
                {
                    Text = "Perfect Faculty!",
                    FacultyGuid = "10",
                    Id = "102"
                });

                dbContext.Reviews.Add(new Review
                {
                    Text = "Perfect VU Faculty!",
                    FacultyGuid = "11",
                    Id = "103"
                });

                dbContext.Reviews.Add(new Review
                {
                    Text = "Nice!",
                    FacultyGuid = "12",
                    Id = "104"
                });

                dbContext.Reviews.Add(new Review
                {
                    Text = "Legit!",
                    FacultyGuid = "13",
                    Id = "105"
                });
            }

            dbContext.SaveChanges();
        }
    }
}