using AutoFixture.Xunit2;
using Moq;
using System.Web.Mvc;
using Webserver.Controllers;
using Webserver.Data.Repositories;
using Xunit;

namespace Webserver.Tests.Controllers
{
    public class ProgrammesControllerTest
    {
        [Theory]
        [AutoMoqData]
        public void Index([Frozen] Mock<IReviewRepository> reviewRepository, [Frozen] Mock<IFacultyRepository> facultyRepository, [Frozen] Mock<IProgrammeRepository> programmerepository)
        {
            var sut = new ProgrammesController(programmerepository.Object, facultyRepository.Object, reviewRepository.Object);

            // Act
            ViewResult result = sut.Index(15) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Theory]
        [AutoMoqData]
        public void Details([Frozen] Mock<IReviewRepository> reviewRepository, [Frozen] Mock<IFacultyRepository> facultyRepository, [Frozen] Mock<IProgrammeRepository> programmerepository)
        {
            var sut = new ProgrammesController(programmerepository.Object, facultyRepository.Object, reviewRepository.Object);

            // Act
            ViewResult result = sut.Details(15) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Theory]
        [AutoMoqData]
        public void Edit([Frozen] Mock<IReviewRepository> reviewRepository, [Frozen] Mock<IFacultyRepository> facultyRepository, [Frozen] Mock<IProgrammeRepository> programmerepository)
        {
            var sut = new ProgrammesController(programmerepository.Object, facultyRepository.Object, reviewRepository.Object);

            // Act
            ViewResult result = sut.Edit(15) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Theory]
        [AutoMoqData]
        public void Add([Frozen] Mock<IReviewRepository> reviewRepository, [Frozen] Mock<IFacultyRepository> facultyRepository, [Frozen] Mock<IProgrammeRepository> programmerepository)
        {
            var sut = new ProgrammesController(programmerepository.Object, facultyRepository.Object, reviewRepository.Object);

            // Act
            ViewResult result = sut.Add(15) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }
    }
}