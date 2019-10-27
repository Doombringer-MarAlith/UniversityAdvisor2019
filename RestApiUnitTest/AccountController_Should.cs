using AutoFixture.Xunit2;
using Dbo;
using Microsoft.AspNetCore.Http;
using Models.Models;
using Debugger;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using Xunit;
using RestApiUnitTest.TestHelpers;
using Moq;
using RestApi.Controllers;
using System.Net;

namespace RestApiUnitTest
{
    public class AccountController_Should
    {
        [Theory]
        [AutoMoqData]
        public void Get_Return_CorrectValue(string id, [Frozen] Mock<ILogger> logger, [Frozen] Account account, [Frozen] Mock<IDatabaseExecutor> database, AccountController controller)
        {
            var serializedAccount = JsonConvert.SerializeObject(account);
            database.Setup(x => x.ReturnAccount(id)).Returns(account);
            var response = controller.Get(id);

            Assert.NotNull(response);
        }
    }
}
