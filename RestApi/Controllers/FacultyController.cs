using Dbo;
using Debugger;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Newtonsoft.Json;
using System;

namespace RestApi.Controllers
{

    [Route("api/faculty")]
    [ApiController]
    public class FacultyController : Controller
    {
        private readonly DatabaseExecutor _database = new DatabaseExecutor();

        [HttpGet("{uniGuid}")]
        public ActionResult<string> Get(string uniGuid)
        {
            Logger.Log($"FacultyController:Get({uniGuid})");

            try
            {
                return JsonConvert.SerializeObject(_database.ReturnFaculties(uniGuid));
            }
            catch (Exception exception)
            {
                Logger.Log($"FacultyController.Get({uniGuid}): DomainError", Level.Error, exception);
                throw;
            }
        }

        [HttpPost("{create}")]
        public void Post([FromBody] Faculty faculty)
        {
            Logger.Log($"FacultyController::Post(Create Faculties)");

            try
            {
                _database.CreateFaculty(faculty);
            }
            catch (Exception exception)
            {
                Logger.Log($"FacultyController.Post(Faculties): DomainError", Level.Error, exception);
                throw;
            }

        }
    }
}