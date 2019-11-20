using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webserver.Models;

namespace Webserver.Data.Repositories
{
    interface IUniversityRepository : IRepository<University>
    {
        University GetUniversityByName(string universityName);
    }
}
