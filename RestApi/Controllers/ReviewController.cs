﻿using Microsoft.AspNetCore.Mvc;
using Dbo;
using Debugger;
using Models.Models;
using Newtonsoft.Json;
using System;

namespace RestApi.Controllers
{

    [Route("api/review")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IDatabaseExecutor _database;

        public ReviewController(IDatabaseExecutor database)
        {
            _database = database;
        }

        [HttpGet("reviewsByGuid/{Guid}/{guidType}")]
        public ActionResult<string> Get(string Guid, int guidType)
        {
            Logger.Log($"ReviewController:Get({Guid}, {guidType})");

            try
            {
                var reviews = _database.ReturnReviews(Guid, guidType);
                if (reviews != null)
                {
                    return Ok(JsonConvert.SerializeObject(reviews));
                }

                return NoContent();
            }
            catch (Exception exception)
            {
                Logger.Log($"ReviewController.Get({Guid}, {guidType}): DomainError", Level.Error, exception);
                throw;
            }
        }

        [HttpGet("{Guid}")]
        public ActionResult<string> Get(string Guid)
        {
            Logger.Log($"ReviewController:Get({Guid})");

            try
            {
                var review = _database.ReturnReview(Guid);
                if (review != null)
                {
                    return Ok(JsonConvert.SerializeObject(review));
                }

                return NoContent();
            }
            catch (Exception exception)
            {
                Logger.Log($"ReviewController.Get({Guid}): DomainError", Level.Error, exception);
                throw;
            }
        }

        [HttpPost("{create}")]
        public void Post([FromBody] Review review)
        {
            Logger.Log($"ReviewController::Post(Create Review)");

            try
            {
                _database.CreateReview(review);
            }
            catch (Exception exception)
            {
                Logger.Log($"ReviewController.Post(Review): DomainError", Level.Error, exception);
                throw;
            }
        }

    }
}