using DboExecutor;
using Debugger;
using Models.Models;
using Newtonsoft.Json;
using System;

namespace WebScripts
{
    public class ReviewController : IReviewController
    {
        private readonly IDatabaseExecutor _database;
        private readonly ILogger _logger;

        public ReviewController(IDatabaseExecutor database, ILogger logger)
        {
            _database = database;
            _logger = logger;
        }

        public string Get(string guid, int guidType)
        {
            _logger.Log($"ReviewController:Get({guid}, {guidType})");

            try
            {
                var reviews = _database.ReturnReviews(guid, guidType);
                if (reviews != null)
                {
                    return JsonConvert.SerializeObject(reviews);
                }

                return null;
            }
            catch (Exception exception)
            {
                _logger.Log($"ReviewController.Get({guid}, {guidType}): DomainError", Level.Error, exception);
                throw;
            }
        }

        public string Get(string guid)
        {
            _logger.Log($"ReviewController:Get({guid})");

            try
            {
                var review = _database.ReturnReview(guid);
                if (review != null)
                {
                    return JsonConvert.SerializeObject(review);
                }

                return null;
            }
            catch (Exception exception)
            {
                _logger.Log($"ReviewController.Get({guid}): DomainError", Level.Error, exception);
                throw;
            }
        }

        public void Post(Review review)
        {
            _logger.Log($"ReviewController::Post(Create Review)");

            try
            {
                _database.CreateReview(review);
            }
            catch (Exception exception)
            {
                _logger.Log($"ReviewController.Post(Review): DomainError", Level.Error, exception);
                throw;
            }
        }
    }
}