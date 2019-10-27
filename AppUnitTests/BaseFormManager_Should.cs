using App;
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
    public class BaseFormManager_Should
    {
        [Theory]
        [AutoMoqData]
        public async Task GetUniversities_IfExist(List<University> universities, string name, [Frozen] Mock<IDataManipulations> dataManipulations, [Frozen] Mock<FormManagerData> formManagerData, UniversitySearchFormManager sut)
        {
            string jsonString = JsonConvert.SerializeObject(universities);
            string url = $"university/{name}";
            dataManipulations.Setup(x => x.GetDataFromServer(It.IsAny<string>())).ReturnsAsync(jsonString);
            var response = await sut.GetUniversities(name);
            dataManipulations.Verify(x => x.GetDataFromServer(It.Is<string>(y => y.Equals(url))), Times.Once);
            Assert.Equal(universities.Select(uni => uni.Name).ToList(), response);
        }

        [Theory]
        [AutoMoqData]
        public async Task GetUniversities_IfNotExist(List<University> universities, string name, [Frozen] Mock<IDataManipulations> dataManipulations, [Frozen] Mock<FormManagerData> formManagerData, UniversitySearchFormManager sut)
        {
            string jsonString = JsonConvert.SerializeObject(universities);
            string url = $"university/{name}";
            string ur = null;
            dataManipulations.Setup(x => x.GetDataFromServer(It.IsAny<string>())).ReturnsAsync(ur);
            var response = await sut.GetUniversities(name);
            dataManipulations.Verify(x => x.GetDataFromServer(It.Is<string>(y => y.Equals(url))), Times.Once);
            Assert.Equal(new List<string>(), response);
        }

        [Theory]
        [AutoMoqData]
        public async Task GetUniversities_IfUnexpectedReturnObject_DoNotCrash(List<University> universities, string name, [Frozen] Mock<IDataManipulations> dataManipulations, [Frozen] Mock<FormManagerData> formManagerData, UniversitySearchFormManager sut)
        {
            string jsonString = JsonConvert.SerializeObject(universities);
            string url = $"university/{name}";
            dataManipulations.Setup(x => x.GetDataFromServer(It.IsAny<string>())).ReturnsAsync("");
            var response = await sut.GetUniversities(name);
            dataManipulations.Verify(x => x.GetDataFromServer(It.Is<string>(y => y.Equals(url))), Times.Once);
            Assert.Equal(new List<string>(), response);
        }
    }
}