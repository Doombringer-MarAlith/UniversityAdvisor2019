using App.FormManagers;
using AppUnitTests.TestHelpers;
using AutoFixture.Xunit2;
using Models.Models;
using Moq;
using Newtonsoft.Json;
using ServerCallFromApp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AppUnitTests
{
    public class SelectedUniversityFormManager_Should
    {
        [Theory]
        [AutoMoqData]
        public async Task GetFaculties_IfExist
        (
            List<Faculty> faculties,
            University university,
            [Frozen] Mock<IDataManipulations> dataManipulations,
            SelectedUniversityFormManager sut
        )
        {
            string jsonString = JsonConvert.SerializeObject(faculties);
            string url = $"faculty/{university.Guid}";

            dataManipulations.Setup(x => x.GetDataFromServer(It.IsAny<string>())).ReturnsAsync(jsonString);

            sut.FormManagerData.SelectedUniversity = university;
            var response = await sut.GetFaculties();

            dataManipulations.Verify(x => x.GetDataFromServer(It.Is<string>(y => y.Equals(url))), Times.Once);

            Assert.Equal(faculties.Select(faculty => faculty.Name).ToList(), response);
        }

        [Theory]
        [AutoMoqData]
        public async Task GetFaculties_IfNotExist
        (
            List<Faculty> faculties,
            University university,
            [Frozen] Mock<IDataManipulations> dataManipulations,
            SelectedUniversityFormManager sut
        )
        {
            string jsonString = JsonConvert.SerializeObject(faculties);
            string url = $"faculty/{university.Guid}";
            string nullString = null;

            dataManipulations.Setup(x => x.GetDataFromServer(It.IsAny<string>())).ReturnsAsync(nullString);

            sut.FormManagerData.SelectedUniversity = university;
            var response = await sut.GetFaculties();

            dataManipulations.Verify(x => x.GetDataFromServer(It.Is<string>(y => y.Equals(url))), Times.Once);

            Assert.Equal(new List<string>(), response);
        }

        [Theory]
        [AutoMoqData]
        public async Task GetFaculties_IfUnexpectedReturnObject_DoNotCrash
        (
            List<Faculty> faculties,
            University university,
            [Frozen] Mock<IDataManipulations> dataManipulations,
            SelectedUniversityFormManager sut
        )
        {
            string jsonString = JsonConvert.SerializeObject(faculties);
            string url = $"faculty/{university.Guid}";

            dataManipulations.Setup(x => x.GetDataFromServer(It.IsAny<string>())).ReturnsAsync("");

            sut.FormManagerData.SelectedUniversity = university;
            var response = await sut.GetFaculties();

            dataManipulations.Verify(x => x.GetDataFromServer(It.Is<string>(y => y.Equals(url))), Times.Once);

            Assert.Equal(new List<string>(), response);
        }
    }
}
