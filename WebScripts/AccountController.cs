using System;
using DboExecutor;
using Debugger;
using Models.Models;
using Newtonsoft.Json;

namespace WebScripts
{
    public class AccountController
    {
        private readonly IDatabaseExecutor _database;
        private readonly ILogger _logger;

        public AccountController(IDatabaseExecutor database , ILogger logger)
        {
            _database = database;
            _logger = logger;
        }

        public string GetAccount(string id)
        {
            _logger.Log($"AccountController:Get({id})");

            try
            {
               var account = _database.ReturnAccount(id);
                if (account != null)
                {
                    return JsonConvert.SerializeObject(account);
                }
                _logger.Log($"AccountController:Get({id}): noContent", Level.Warning);
                return null;
            }
            catch (Exception exception)
            {
                _logger.Log($"AccountController.Get({id}): DomainError", Level.Error, exception);
                throw;
            }
        }

        public string GetGuidByEmail(string email)
        {
            _logger.Log($"AccountController:Check({email})");

            try
            {
                var guid = _database.CheckAccountEmail(email);
                if (guid != null)
                {
                    return guid;
                }

                return null;
            }
            catch (Exception exception)
            {
                _logger.Log($"AccountController.Check({email}): DomainError", Level.Error, exception);
                throw;
            }
        }

        public string GetBuidByUserName(string username)
        {
            _logger.Log($"AccountController:Check({username})");

            try
            {
                var guid = _database.CheckAccountUsername(username);
                if (guid != null)
                {
                    return guid;
                }

                return null; 
            }
            catch (Exception exception)
            {
                _logger.Log($"AccountController.Check({username}): DomainError", Level.Error, exception);
                throw;
            }
        }

        public string GetGuidByEmailAndPassword(string email, string password)
        {
            _logger.Log($"AccountController.Get({email} ,  {password}) ");

            try
            {
                var guid = _database.ReturnAccountGuid(email, password);
                if (guid != null)
                {
                    return guid;
                }

                return null; 
            }
            catch (Exception exception)
            {
                _logger.Log($"AccountController::Get(): DomainError", Level.Error, exception);
                throw;
            }
        }

        public void PostAccount(Account account)
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
