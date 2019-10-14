using System;
using System.Net.Http;
using App;
using Microsoft.Extensions.DependencyInjection;
using ServerCallFromApp;

namespace Objektinis
{
    public class ContainerBuilder
    {
        public IServiceProvider Build()
        {
            var container = new ServiceCollection();
            container.AddSingleton<IDataManipulations, DataManipulations>();
            container.AddSingleton<HttpClient>();
            container.AddSingleton<IUniversitySearchForm,UniversitySearchForm>();
            container.AddSingleton<ILoginForm, LoginForm>();
            container.AddSingleton<IReviewForm, ReviewForm>();
            container.AddSingleton<ISelectedUniversityForm, SelectedUniversityForm>();
            return container.BuildServiceProvider();
        }
    }
}
