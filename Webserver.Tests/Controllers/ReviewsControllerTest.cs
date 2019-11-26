using AutoFixture.Xunit2;
using Models;
using Moq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Webserver.Controllers;
using Webserver.Data.Repositories;
using Xunit;

namespace Webserver.Tests.Controllers
{
    public class ReviewsControllerTest
    {
        [Theory]
        [AutoMoqData]
        public void University([Frozen] Mock<IReviewRepository> reviewRepository)
        {
            var sut = new ReviewsController(reviewRepository.Object);
            // Act
            ViewResult result = sut.University(15) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Theory]
        [AutoMoqData]
        public void Faculty([Frozen] Mock<IReviewRepository> reviewRepository)
        {
            var sut = new ReviewsController(reviewRepository.Object);
            // Act
            ViewResult result = sut.Faculty(15) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Theory]
        [AutoMoqData]
        public void Programme([Frozen] Mock<IReviewRepository> reviewRepository)
        {
            var sut = new ReviewsController(reviewRepository.Object);
            // Act
            ViewResult result = sut.Programme(15) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Theory]
        [AutoMoqData]
        public void Details([Frozen] Mock<IReviewRepository> reviewRepository)
        {
            var sut = new ReviewsController(reviewRepository.Object);
            // Act
            ViewResult result = sut.Details(15) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Theory]
        [AutoMoqData]
        public void Delete([Frozen] Mock<IReviewRepository> reviewRepository)
        {
            var sut = new ReviewsController(reviewRepository.Object);
            // Act
            ViewResult result = sut.Delete(15) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Theory]
        [AutoMoqData]
        public void Edit([Frozen] Mock<IReviewRepository> reviewRepository)
        {
            var sut = new ReviewsController(reviewRepository.Object);
            // Act
            ViewResult result = sut.Edit(15) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }
    }
}