using Dbo;
using Debugger;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Newtonsoft.Json;
using System;

namespace RestApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly DatabaseExecutor _database = new DatabaseExecutor();

        [HttpGet("{id}")]
        public ActionResult<string> Get(string id)
        {
            Logger.Log($"AccountController:Get({id})");

            try
            {
                return JsonConvert.SerializeObject(_database.ReturnAccount(id));
            }
            catch (Exception exception)
            {
                Logger.Log($"AccountController.Get({id}): DomainError", Level.Error, exception);
                throw;
            }
        }

        [HttpGet("login/{email}/{password}")]
        public ActionResult<string> Get(string email, string password)
        {
            Logger.Log($"AccountController.Get({email} ,  {password}) ");

            try
            {
                return _database.ReturnAccountGuid(email, password);
            }
            catch (Exception exception)
            {
                Logger.Log($"AccountController::Get(): DomainError", Level.Error, exception);
                throw;
            }
        }

        [HttpPost("create")]
        public void Post([FromBody] Account account)
        {
            Logger.Log($"AccountController::Post(Create Account)");

            try
            {
                _database.CreateAccount(account);
            }
            catch (Exception exception)
            {
                Logger.Log($"AccountController.Post(Account): DomainError", Level.Error, exception);
                throw;
            }
        }
    }
}
