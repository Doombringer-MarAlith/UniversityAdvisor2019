using AutoFixture.Xunit2;
using Moq;
using System.Web.Mvc;
using Webserver.Controllers;
using Webserver.Data.Repositories;
using Xunit;

namespace Webserver.Tests.Controllers
{
    public class ReviewControllerTest
    {
        [Theory]
        [AutoMoqData]
        public void Index([Frozen] Mock<IReviewRepository> reviewRepository)
        {
            var sut = new AddReviewController(reviewRepository.Object);

            // Act
            ViewResult result = sut.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Theory]
        [AutoMoqData]
        public void UniversityReview([Frozen] Mock<IReviewRepository> reviewRepository)
        {
            var sut = new AddReviewController(reviewRepository.Object);

            // Act
            ViewResult result = sut.UniversityReview(15) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Theory]
        [AutoMoqData]
        public void FacultyReview([Frozen] Mock<IReviewRepository> reviewRepository)
        {
            var sut = new AddReviewController(reviewRepository.Object);

            // Act
            ViewResult result = sut.FacultyReview(14) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Theory]
        [AutoMoqData]
        public void ProgrammeReview([Frozen] Mock<IReviewRepository> reviewRepository)
        {
            var sut = new AddReviewController(reviewRepository.Object);

            // Act
            ViewResult result = sut.ProgrammeReview(14) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }
    }
}