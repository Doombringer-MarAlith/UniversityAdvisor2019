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
        private readonly ILogger _logger;

        public AccountController(IDatabaseExecutor database , ILogger logger)
        {
            _database = database;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(string id)
        {
            _logger.Log($"AccountController:Get({id})");

            try
            {
               var account = _database.ReturnAccount(id);
                if (account != null)
                {
                    return Ok(JsonConvert.SerializeObject(account));
                }
                _logger.Log($"AccountController:Get({id}): noContent", Level.Warning);
                return NoContent();
            }
            catch (Exception exception)
            {
                _logger.Log($"AccountController.Get({id}): DomainError", Level.Error, exception);
                throw;
            }
        }

        [HttpGet("checkByEmail/{email}/{whatever}")]
        public ActionResult<string> Get(string email, bool whatever)
        {
            _logger.Log($"AccountController:Check({email})");

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
                _logger.Log($"AccountController.Check({email}): DomainError", Level.Error, exception);
                throw;
            }
        }

        [HttpGet("checkByUsername/{username}/{whatever}")]
        public ActionResult<string> Get(string username, int whatever)
        {
            _logger.Log($"AccountController:Check({username})");

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
                _logger.Log($"AccountController.Check({username}): DomainError", Level.Error, exception);
                throw;
            }
        }

        [HttpGet("login/{email}/{password}")]
        public ActionResult<string> Get(string email, string password)
        {
            _logger.Log($"AccountController.Get({email} ,  {password}) ");

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
                _logger.Log($"AccountController::Get(): DomainError", Level.Error, exception);
                throw;
            }
        }

        [HttpPost("create")]
        public void Post([FromBody] Account account)
        {
            _logger.Log($"AccountController::Post(Create Account)");

            try
            {
                _database.CreateAccount(account);
            }
            catch (Exception exception)
            {
                _logger.Log($"AccountController.Post(Account): DomainError", Level.Error, exception);
                throw;
            }
        }
    }
}
