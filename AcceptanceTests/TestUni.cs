﻿using ExternalDependencies;
using Models.Models;
using Newtonsoft.Json;
using ServerCallFromApp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AcceptanceTests
{
    public class TestUni
    {
        [Fact]
        public async Task Create_Uni_AndGetUni()
        {
            DataManipulations dataManipulations = new DataManipulations(new HttpInternalClient());
            string newGuid = Guid.NewGuid().ToString();
            var uni = new University()
            {
                Name = Guid.NewGuid().ToString(),
                Guid = newGuid
            };

            await dataManipulations.PostDataToServer("university/create", JsonConvert.SerializeObject(uni));
            var returnUnis = JsonConvert.DeserializeObject<List<University>>(await dataManipulations.GetDataFromServer($"university/{uni.Name}"));
            Assert.NotNull(returnUnis);
            /*foreach (University un in returnUnis)
            {
                Assert.Equal(un, uni);
            }*/
        }
    }
}