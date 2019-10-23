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
        private readonly IDatabaseExecutor _database;

        public FacultyController(IDatabaseExecutor database)
        {
            _database = database;
        }

        [HttpGet("{uniGuid}")]
        public ActionResult<string> Get(string uniGuid)
        {
            Logger.Log($"FacultyController:Get({uniGuid})");

            try
            {
                var faculties = _database.ReturnFaculties(uniGuid);
                if (faculties != null)
                {
                    return Ok(JsonConvert.SerializeObject(faculties));
                }

                return NoContent();
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