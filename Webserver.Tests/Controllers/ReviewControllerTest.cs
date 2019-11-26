using System.Net.Http;
using System.Web.Mvc;
using Webserver.Controllers;
using Webserver.Data.Infrastructure;
using Webserver.Data.Repositories;
using Xunit;

namespace Webserver.Tests.Controllers
{
    public class ReviewControllerTest
    {
        [Fact]
        public void Index()
        {
            // Arrange
            var controller = new AddReviewController(new ReviewRepository(new DatabaseFactory()));

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }
    }
}