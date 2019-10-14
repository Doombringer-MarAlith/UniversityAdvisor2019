using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using ServerCallFromApp;

namespace RestApi
{
    public class ContainerBuild
    {
        public IServiceProvider Build()
        {
            var container = new ServiceCollection();
            container.AddSingleton<IDataManipulations, DataManipulations>();
            container.AddSingleton<HttpClient>();
            return container.BuildServiceProvider();
        }
    }
}
