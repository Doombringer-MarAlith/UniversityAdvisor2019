using System;
using System.Net.Http;
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
            container.AddSingleton<HttpClient>();
            container.AddSingleton<IUniversitySearchForm,UniversitySearchForm>();
            container.AddTransient<ILoginForm>(s => new LoginForm()); //container.AddSingleton<ILoginForm, LoginForm>();
            container.AddSingleton<IReviewForm, ReviewForm>();
            container.AddSingleton<ISelectedUniversityForm, SelectedUniversityForm>();
            return container.BuildServiceProvider();
        }
    }
}
