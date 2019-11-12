using System;
using System.Threading.Tasks;
using DboExecutor;
using Debugger;
using Models.Models;
using Newtonsoft.Json;

namespace WebScripts
{
    public class UniversityController : IUniversityController
    {
        private readonly IDatabaseExecutor _database;
        private readonly ILogger _logger;

        public UniversityController(IDatabaseExecutor database, ILogger logger)
        {
            _database = database;
            _logger = logger;
        }

        public async Task<string> Get(string name)
        {
            _logger.Log($"UniversityController:Get({name})");

            try
            {
                var universities = _database.ReturnUniversities(name);
                if (universities != null)
                {
                    return JsonConvert.SerializeObject(universities);
                }

                return null;
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

        public void Post(University university)
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