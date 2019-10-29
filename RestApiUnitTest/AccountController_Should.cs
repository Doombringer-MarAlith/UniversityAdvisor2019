using AutoFixture.Xunit2;
using Dbo;
using Debugger;
using Models.Models;
using Moq;
using Newtonsoft.Json;
using RestApi.Controllers;
using RestApiUnitTest.TestHelpers;
using Xunit;

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
            database.Verify(x => x.ReturnAccount(id), Times.Once);
            Assert.NotNull(response);
        }

        [Theory]
        [InlineData("0")]
        [InlineData("1")]
        [InlineData("2")]
        [InlineData("3")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("0874645365132af35f131sadf2sad1fsa35fd543fa1sd0a13f15sa456sad1f35sad1fs2d1fds31fs5d4f3sda12f1sadf2asdf2s3ad1fsaf")]
        [InlineData("-0874645365132af35f131sadf2sad1fsa35fd543fa1")]
        [InlineData("0874645365132af35f131sadf2sad1fsa35fd543fa1")]
        [InlineData("efsaiah")]
        [InlineData("fdasf0")]
        [InlineData("fsad0")]
        [InlineData("fdasewrq0")]
        [InlineData("fsewqewrqerwqrwqrad0")]
        [InlineData(",,,,,,,,,,,,,,,,,,,,,,,")]
        [InlineData("............................")]
        [InlineData("rw                                                     qf0")]
        [InlineData("few0")]
        [InlineData("____________________________________")]
        [InlineData("few0")]
        [InlineData("rwqe0")]
        [InlineData("ewqfewfq0")]
        [InlineData("/////////////////////////////")]
        [InlineData("0fewqf                                                                                                                 wqf")]
        [InlineData("rweqqr             w0")]
        [InlineData("0wrqerwqe")]
        [InlineData("                                                         k                                                     0")]
        [InlineData("????????????????????????????????????")]
        public void Get_DontCrash_WithValue(string id)
        {
            Mock<ILogger> logger = new Mock<ILogger>();
            Account account = new Account();
            Mock<IDatabaseExecutor> database = new Mock<IDatabaseExecutor>();
            AccountController controller = new AccountController(new DatabaseExecutor(new Logger()), new Logger());
            var serializedAccount = JsonConvert.SerializeObject(account);

            database.Setup(x => x.ReturnAccount(id)).Returns(account);
            var response = controller.Get(id);
            Assert.NotNull(response);
        }
    }
}