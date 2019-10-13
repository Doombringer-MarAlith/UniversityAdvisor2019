using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dbo;
using Debugger;
using Models.Models;
using Newtonsoft.Json;
using System;

namespace RestApi.Controllers
{
   
        [Route("api/uniReview")]
        [ApiController]
        public class UniReviewController : ControllerBase
        {
            private readonly DatabaseExecutor _database = new DatabaseExecutor();

            [HttpGet("{Text}")]
            public ActionResult<string> Get(string text)
            {
                Logger.Log($"UniReviewController:Get({text})");

                try
                {
                    return JsonConvert.SerializeObject(_database.ReturnUniversities(text));
                }
                catch (Exception exception)
                {
                    Logger.Log($"UniversityController.Get({text}): DomainError", Level.Error, exception);
                    throw;
                }
            }
          
            [HttpPost("{create}")]
            public void Post([FromBody] Review review)
            {
                Logger.Log($"UniReviewController::Post(Create Review)");

                try
                {
                    _database.CreateReview(review);
                }
                catch (Exception exception)
                {
                    Logger.Log($"UniReviewController.Post(Review): DomainError", Level.Error, exception);
                    throw;
                }
            }

        }
}