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

            [HttpGet("{uniGuid}")]
            public ActionResult<string> Get(string uniGuid)
            {
                Logger.Log($"UniReviewController:Get({uniGuid})");

                try
                {
                    return JsonConvert.SerializeObject(_database.ReturnReviews(uniGuid));
                }
                catch (Exception exception)
                {
                    Logger.Log($"uniReviewController.Get({uniGuid}): DomainError", Level.Error, exception);
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