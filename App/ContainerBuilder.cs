using System;
using System.Net.Http;
using ExternalDependencies;
using Microsoft.Extensions.DependencyInjection;
using ServerCallFromApp;

namespace App
{
    public class ContainerBuilder
    {
        public IServiceProvider Build()
        {
            var container = new ServiceCollection();
            container.AddSingleton<IDataManipulations, DataManipulations>();
            container.AddSingleton<IHttpInternalClient, HttpInternalClient>();
            container.AddSingleton<IUniversitySearchForm,UniversitySearchForm>();
            container.AddTransient<ILoginForm>(s => new LoginForm()); //container.AddSingleton<ILoginForm, LoginForm>();
            container.AddSingleton<IReviewForm, WriteReviewForm>();
            container.AddSingleton<ISelectedUniversityForm, SelectedUniversityForm>();
            return container.BuildServiceProvider();
        }
    }
}
