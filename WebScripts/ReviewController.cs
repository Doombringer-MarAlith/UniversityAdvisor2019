using System;

namespace WebScripts
{

    [Route("api/review")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IDatabaseExecutor _database;
        private readonly ILogger _logger;

        public ReviewController(IDatabaseExecutor database , ILogger logger)
        {
            _database = database;
            _logger = logger;
        }

        [HttpGet("reviewsByGuid/{Guid}/{guidType}")]
        public ActionResult<string> Get(string Guid, int guidType)
        {
            _logger.Log($"ReviewController:Get({Guid}, {guidType})");

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
                _logger.Log($"ReviewController.Get({Guid}, {guidType}): DomainError", Level.Error, exception);
                throw;
            }
        }

        [HttpGet("{Guid}")]
        public ActionResult<string> Get(string Guid)
        {
            _logger.Log($"ReviewController:Get({Guid})");

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
                _logger.Log($"ReviewController.Get({Guid}): DomainError", Level.Error, exception);
                throw;
            }
        }

        [HttpPost("{create}")]
        public void Post([FromBody] Review review)
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