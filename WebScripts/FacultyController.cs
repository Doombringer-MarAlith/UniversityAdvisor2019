using System;
using DboExecutor;
using Debugger;
using Models.Models;
using Newtonsoft.Json;

namespace WebScripts
{

    public class FacultyController
    {
        private readonly IDatabaseExecutor _database;
        private readonly ILogger _logger;

        public FacultyController(IDatabaseExecutor database , ILogger logger)
        {
            _database = database;
            _logger = logger;
        }
        public string Get(string uniGuid)
        {
            _logger.Log($"FacultyController:Get({uniGuid})");

            try
            {
                var faculties = _database.ReturnFaculties(uniGuid);
                if (faculties != null)
                {
                    return JsonConvert.SerializeObject(faculties);
                }

                return null;
            }
            catch (Exception exception)
            {
                _logger.Log($"FacultyController.Get({uniGuid}): DomainError", Level.Error, exception);
                throw;
            }
        }

        public void Post(Faculty faculty)
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