using Dbo;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Newtonsoft.Json;

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
            return JsonConvert.SerializeObject(_database.ReturnAccount(id));
        }

        [HttpPost("create")]
        public void Post([FromBody] Account account)
        {
            _database.CreateAccount(account);
        }
    }
}