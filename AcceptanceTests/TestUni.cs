using System;
using System.Collections.Generic;
using AcceptanceTests.TestHelpers;
using Models.Models;
using Newtonsoft.Json;
using ServerCallFromApp;
using Xunit;

namespace AcceptanceTests
{
    public class TestUni
    {
        [Fact]
        public void Create_Uni_AndGetUni()
        {
            string newGuid = Guid.NewGuid().ToString();
            var uni = new University()
            {
                Name = "Vilniaus Universitetas",
                Guid = newGuid
            };

            DataManipulations.PostDataToServer("university/create", JsonConvert.SerializeObject(uni));
            var returnUnis = JsonConvert.DeserializeObject<List<University>>(DataManipulations.GetDataFromServer($"university/{uni.Name}"));
            Assert.NotNull(returnUnis);
            foreach(University un in returnUnis)
            {
                if (un.Equals(uni))
                {
                    Console.WriteLine("Found one");
                }
            }
            //Assert.Equal(newGuid, returnUni.Guid);

            //var fetchedUni = JsonConvert.DeserializeObject<University>(DataManipulations.GetDataFromServer($"university/{newGuid}"));
            //Assert.Equal(fetchedUni.Guid, uni.Guid);
            //Assert.Equal(fetchedUni.Name, uni.Name);
        }
    }
}
