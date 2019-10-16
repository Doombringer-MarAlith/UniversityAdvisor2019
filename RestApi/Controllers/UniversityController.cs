using Microsoft.AspNetCore.Mvc;
using Dbo;
using Debugger;
using Models.Models;
using Newtonsoft.Json;
using System;

namespace RestApi.Controllers
{
    [Route("api/university")]
    [ApiController]
    public class UniversityController : ControllerBase
    {
        private readonly DatabaseExecutor _database = new DatabaseExecutor();

        [HttpGet("{name}")]
        public ActionResult<string> Get(string name)
        {
            Logger.Log($"UniversityController:Get({name})");

            try
            {
                return JsonConvert.SerializeObject(_database.ReturnUniversities(name));
            }
            catch (Exception exception)
            {
                Logger.Log($"UniversityController.Get({name}): DomainError", Level.Error, exception);
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
            Logger.Log($"UniversityController::Post(Create University)");

            try
            {
                _database.CreateUniversity(university);
            }
            catch (Exception exception)
            {
                Logger.Log($"UniversityController.Post(University): DomainError", Level.Error, exception);
                throw;
            }
        }

    }
}