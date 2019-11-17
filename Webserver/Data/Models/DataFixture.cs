using System;
using System.Linq;
using Webserver.Models;

namespace Webserver.Data.Models
{
    public class DataFixture
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
                    Id = "asdsadasd"
                });
            }

            dbContext.SaveChanges();
        }
    }
}