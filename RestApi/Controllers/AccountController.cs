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
        private readonly IDatabaseExecutor _database;

        public AccountController(IDatabaseExecutor database)
        {
            _database = database;
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(string id)
        {
            Logger.Log($"AccountController:Get({id})");

            try
            {
                var account = _database.ReturnAccount(id);
                if (account != null)
                {
                    return Ok(JsonConvert.SerializeObject(account));
                }

                return NoContent();
            }
            catch (Exception exception)
            {
                Logger.Log($"AccountController.Get({id}): DomainError", Level.Error, exception);
                throw;
            }
        }

        [HttpGet("checkByEmail/{email}/{whatever}")]
        public ActionResult<string> Get(string email, bool whatever)
        {
            Logger.Log($"AccountController:Check({email})");

            try
            {
                var guid = _database.CheckAccountEmail(email);
                if (guid != null)
                {
                    return Ok(guid);
                }

                return NoContent();
            }
            catch (Exception exception)
            {
                Logger.Log($"AccountController.Check({email}): DomainError", Level.Error, exception);
                throw;
            }
        }

        [HttpGet("checkByUsername/{username}/{whatever}")]
        public ActionResult<string> Get(string username, int whatever)
        {
            Logger.Log($"AccountController:Check({username})");

            try
            {
                var guid = _database.CheckAccountUsername(username);
                if (guid != null)
                {
                    return Ok(guid);
                }

                return NoContent(); 
            }
            catch (Exception exception)
            {
                Logger.Log($"AccountController.Check({username}): DomainError", Level.Error, exception);
                throw;
            }
        }

        [HttpGet("login/{email}/{password}")]
        public ActionResult<string> Get(string email, string password)
        {
            Logger.Log($"AccountController.Get({email} ,  {password}) ");

            try
            {
                var guid = _database.ReturnAccountGuid(email, password);
                if (guid != null)
                {
                    return Ok(guid);
                }

                return NoContent(); 
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
