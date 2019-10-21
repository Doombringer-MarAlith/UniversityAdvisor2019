using System;
using App;
using ExternalDependencies;
using Microsoft.Extensions.DependencyInjection;
using Objektinis.FormManagers;
using ServerCallFromApp;

namespace Objektinis
{
    internal class ContainerBuilder
    {
        readonly ServiceCollection _container = new ServiceCollection();

        public ContainerBuilder()
        {
            _container.AddSingleton<IDataManipulations, DataManipulations>();
            _container.AddSingleton<IHttpInternalClient, HttpInternalClient>();
            _container.AddSingleton(x => new FormManagerData());

            _container.AddSingleton<ILoginFormManager,LoginFormManager>();
            _container.AddSingleton<IReadReviewFormManager,ReadReviewFormManager>();
            _container.AddSingleton<ISelectedUniversityFormManager,SelectedUniversityFormManager>();
            _container.AddSingleton<ISignUpFormManager,SignUpFormManager>();
            _container.AddSingleton<IUniversitySearchFormManager,UniversitySearchFormManager>();
            _container.AddSingleton<IWriteReviewFormManager,WriteReviewFormManager>();
            //-----------------
            _container.AddSingleton<IBaseFormManager, BaseFormManager>();
            _container.AddSingleton<IUniversitySearchForm, UniversitySearchForm>();
            _container.AddSingleton<ILoginForm, LoginForm>();
            _container.AddSingleton<IWriteReviewForm, WriteReviewForm>();
            _container.AddSingleton<ISelectedUniversityForm, SelectedUniversityForm>();
            _container.AddSingleton<IReadReviewForm,ReadReviewForm>();
            _container.AddSingleton<ISignUpForm, SignUpForm>();

            //-----------------
      
            
        }
        public IServiceProvider Build()
        {
            return _container.BuildServiceProvider();
        }
    }
}