using AutoFixture.Xunit2;
using Models;
using Moq;
using System.Web.Mvc;
using Webserver.Controllers;
using Webserver.Data.Repositories;
using Webserver.Enums;
using Webserver.Services;
using Xunit;

namespace Webserver.Tests.Controllers
{
    public class ReviewsControllerTest
    {
        [Theory]
        [AutoMoqData]
        public void University([Frozen] Mock<IReviewRepository> reviewRepository, [Frozen] Mock<IPaginationHandler<Review, ReviewSortOrder>> paginationHandler)
        {
            var sut = new ReviewsController(reviewRepository.Object, paginationHandler.Object);

            // Act
            ViewResult result = sut.University(15, null) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Theory]
        [AutoMoqData]
        public void Faculty([Frozen] Mock<IReviewRepository> reviewRepository, [Frozen] Mock<IPaginationHandler<Review, ReviewSortOrder>> paginationHandler)
        {
            var sut = new ReviewsController(reviewRepository.Object, paginationHandler.Object);

            // Act
            ViewResult result = sut.Faculty(15, null) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Theory]
        [AutoMoqData]
        public void Programme([Frozen] Mock<IReviewRepository> reviewRepository, [Frozen] Mock<IPaginationHandler<Review, ReviewSortOrder>> paginationHandler)
        {
            var sut = new ReviewsController(reviewRepository.Object, paginationHandler.Object);

            // Act
            ViewResult result = sut.Programme(15, null) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Theory]
        [AutoMoqData]
        public void Details([Frozen] Mock<IReviewRepository> reviewRepository, [Frozen] Mock<IPaginationHandler<Review, ReviewSortOrder>> paginationHandler)
        {
            var sut = new ReviewsController(reviewRepository.Object, paginationHandler.Object);

            // Act
            ViewResult result = sut.Details(15) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Theory]
        [AutoMoqData]
        public void Delete([Frozen] Mock<IReviewRepository> reviewRepository, [Frozen] Mock<IPaginationHandler<Review, ReviewSortOrder>> paginationHandler)
        {
            var sut = new ReviewsController(reviewRepository.Object, paginationHandler.Object);

            // Act
            ViewResult result = sut.Delete(15) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Theory]
        [AutoMoqData]
        public void Edit([Frozen] Mock<IReviewRepository> reviewRepository, [Frozen] Mock<IPaginationHandler<Review, ReviewSortOrder>> paginationHandler)
        {
            var sut = new ReviewsController(reviewRepository.Object, paginationHandler.Object);

            // Act
            ViewResult result = sut.Edit(15) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }
    }
}