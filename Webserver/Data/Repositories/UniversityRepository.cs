using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webserver.Models;

namespace Webserver.Data.Repositories
{
    public class UniversityRepository : RepositoryBase<University>, IUniversityRepository
    {
        public UniversityRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        private ApplicationDbContext _dbContext { get; set; }

        public void AddUniversity(University university)
        {
            //_dbContext.Universities.Add();
        }

        public University GetUniversityByName(string universityName)
        {
            return null;
        }
    }
}