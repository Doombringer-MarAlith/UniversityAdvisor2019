using ExternalDependencies;
using Microsoft.Extensions.DependencyInjection;
using ServerCallFromApp;
using System;
using Objektinis;

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
            container.AddSingleton<IBaseFormManager,BaseFormManager>();
            container.AddSingleton<ILoginFormManager,LoginFormManager>();
            container.AddSingleton<IReadReviewFormManager,ReadReviewFormManager>();
            container.AddSingleton<ISelectedUniversityFormManager,SelectedUniversityFormManager>();
            container.AddSingleton<ISignUpFormManager,SignUpFormManager>();
            container.AddSingleton<IUniversitySearchFormManager,UniversitySearchFormManager>();
            container.AddSingleton<IWriteReviewFormManager,WriteReviewFormManager>();
            container.AddSingleton(x => new FormManagerData());
            return container.BuildServiceProvider();
        }
    }
}