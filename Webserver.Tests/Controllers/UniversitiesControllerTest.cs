using AutoFixture.Xunit2;
using Moq;
using System.Web.Mvc;
using Webserver.Controllers;
using Webserver.Data.Repositories;
using Webserver.Services.Api;
using Xunit;

namespace Webserver.Tests.Controllers
{
    public class UniversitiesControllerTest
    {
        [Theory]
        [AutoMoqData]
        public void Index([Frozen] Mock<IReviewRepository> reviewRepository, [Frozen] Mock<IUniversityRepository> universityRepository, [Frozen] Mock<IMapsApi> maps)
        {
            var sut = new UniversitiesController(universityRepository.Object, reviewRepository.Object, maps.Object);

            // Act
            ViewResult result = sut.Index(null, "") as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Theory]
        [AutoMoqData]
        public void Details([Frozen] Mock<IReviewRepository> reviewRepository, [Frozen] Mock<IUniversityRepository> universityRepository, [Frozen] Mock<IMapsApi> maps)
        {
            var sut = new UniversitiesController(universityRepository.Object, reviewRepository.Object, maps.Object);

            // Act
            ViewResult result = sut.Details(15) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }
    }
}