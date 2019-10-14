using System;
using System.ComponentModel.Design;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using App;
using ServerCallFromApp;
namespace DependencyInjection
{
    public class ContainerBuild
    {
        public IServiceProvider Build()
        {
            var container = new ServiceCollection();
            container.AddSingleton<IDataManipulations, DataManipulations>();
            container.AddSingleton<HttpClient>();
            container.AddSingleton<UniversitySearchForm>();
            return container.BuildServiceProvider();
        }
    }
}
