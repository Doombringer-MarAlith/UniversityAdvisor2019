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
        private readonly ILogger _logger;

        public FacultyController(IDatabaseExecutor database , ILogger logger)
        {
            _database = database;
            _logger = logger;
        }

        [HttpGet("{uniGuid}")]
        public ActionResult<string> Get(string uniGuid)
        {
            _logger.Log($"FacultyController:Get({uniGuid})");

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
                _logger.Log($"FacultyController.Get({uniGuid}): DomainError", Level.Error, exception);
                throw;
            }
        }

        [HttpPost("{create}")]
        public void Post([FromBody] Faculty faculty)
        {
            _logger.Log($"FacultyController::Post(Create Faculties)");

            try
            {
                _database.CreateFaculty(faculty);
            }
            catch (Exception exception)
            {
                _logger.Log($"FacultyController.Post(Faculties): DomainError", Level.Error, exception);
                throw;
            }

        }
    }
}