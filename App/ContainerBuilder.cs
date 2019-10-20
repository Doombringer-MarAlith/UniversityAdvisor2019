using ExternalDependencies;
using Microsoft.Extensions.DependencyInjection;
using ServerCallFromApp;
using System;

namespace App
{
    public class ContainerBuilder
    {
        public IServiceProvider Build()
        {
            var container = new ServiceCollection();
            container.AddSingleton<IDataManipulations, DataManipulations>();
            container.AddSingleton<IHttpInternalClient, HttpInternalClient>();
            container.AddSingleton<IUniversitySearchForm, UniversitySearchForm>();
            container.AddSingleton<ILoginForm, LoginForm>();
            container.AddSingleton<IReviewForm, WriteReviewForm>();
            container.AddSingleton<ISelectedUniversityForm, SelectedUniversityForm>();
            return container.BuildServiceProvider();
        }
    }
}