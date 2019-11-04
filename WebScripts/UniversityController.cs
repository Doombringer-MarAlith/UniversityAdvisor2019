using System;
using System.Threading.Tasks;
using DboExecutor;
using Debugger;
using Models.Models;
using Newtonsoft.Json;

namespace WebScripts
{
    [Route("api/university")]
    [ApiController]
    public class UniversityController : ControllerBase
    {
        private readonly IDatabaseExecutor _database;
        private readonly ILogger _logger;

        public UniversityController(IDatabaseExecutor database, ILogger logger)
        {
            _database = database;
            _logger = logger;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            _logger.Log($"UniversityController:Get({name})");

            try
            {
                var universities = _database.ReturnUniversities(name);
                if (universities != null)
                {
                    return Ok(JsonConvert.SerializeObject(universities));
                }

                return NoContent();
            }
            catch (Exception exception)
            {
                _logger.Log($"UniversityController.Get({name}): DomainError", Level.Error, exception);
                throw;
            }
        }
        /*
        [HttpGet("name/{guid}")]
        public ActionResult<string> Get(string name)
        {
            Logger.Log($"UniversityController:Get({name})");

            try
            {
                return JsonConvert.SerializeObject(_database.ReturnUniversity(guid));
            }
            catch (Exception exception)
            {
                Logger.Log($"UniversityController.Get({guid}): DomainError", Level.Error, exception);
                throw;
            }
        }
        */
        [HttpPost("{create}")]
        public void Post([FromBody] University university)
        {
            _logger.Log($"UniversityController::Post(Create University)");

            try
            {
                _database.CreateUniversity(university);
            }
            catch (Exception exception)
            {
                _logger.Log($"UniversityController.Post(University): DomainError", Level.Error, exception);
                throw;
            }
        }

    }
}