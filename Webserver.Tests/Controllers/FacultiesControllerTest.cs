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
    public class FacultiesControllerTest
    {
        [Theory]
        [AutoMoqData]
        public void Index([Frozen] Mock<IReviewRepository> reviewRepository, [Frozen] Mock<IFacultyRepository> facultyRepository, [Frozen] Mock<IUniversityRepository> universityRepository)
        {
            var sut = new FacultiesController(facultyRepository.Object, universityRepository.Object, reviewRepository.Object);
            // Act
            ViewResult result = sut.Index(15) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Theory]
        [AutoMoqData]
        public void Details([Frozen] Mock<IReviewRepository> reviewRepository, [Frozen] Mock<IFacultyRepository> facultyRepository, [Frozen] Mock<IUniversityRepository> universityRepository)
        {
            var sut = new FacultiesController(facultyRepository.Object, universityRepository.Object, reviewRepository.Object);
            // Act
            ViewResult result = sut.Details(15) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Theory]
        [AutoMoqData]
        public void Edit([Frozen] Mock<IReviewRepository> reviewRepository, [Frozen] Mock<IFacultyRepository> facultyRepository, [Frozen] Mock<IUniversityRepository> universityRepository)
        {
            var sut = new FacultiesController(facultyRepository.Object, universityRepository.Object, reviewRepository.Object);
            // Act
            ViewResult result = sut.Edit(15) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Theory]
        [AutoMoqData]
        public void Add([Frozen] Mock<IReviewRepository> reviewRepository, [Frozen] Mock<IFacultyRepository> facultyRepository, [Frozen] Mock<IUniversityRepository> universityRepository)
        {
            var sut = new FacultiesController(facultyRepository.Object, universityRepository.Object, reviewRepository.Object);
            // Act
            ViewResult result = sut.Add(15) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }
    }
}