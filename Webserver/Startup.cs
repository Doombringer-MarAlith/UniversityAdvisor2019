using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Models;
using Owin;
using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using Webserver.Data.Infrastructure;
using Webserver.Data.Repositories;
using Webserver.Enums;
using Webserver.Helpers;
using Webserver.Models;
using Webserver.Data.Services;
using Webserver.Services;
using Webserver.Services.Api;
using WebScraper;

[assembly: OwinStartupAttribute(typeof(Webserver.Startup))]

namespace Webserver
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Scraper>()
                .As<IGatherDatabase>()
                .SingleInstance()
                .WithParameter(new TypedParameter(typeof(bool), ConfigurationManager.AppSettings["Environment"].ToString() == "Production"));

            builder.RegisterType<DatabaseFiller>()
                .As<IDatabaseFiller>()
                .SingleInstance();

            builder.RegisterType<DatabaseFactory>()
                .As<IDatabaseFactory>()
                .SingleInstance();

            builder.RegisterType<ApplicationUserRepository>()
                .As<IUserStore<ApplicationUser>>()
                .InstancePerRequest();

            builder.RegisterType<UniversityRepository>()
                .As<IUniversityRepository>()
                .InstancePerRequest();

            builder.RegisterType<FacultyRepository>()
                .As<IFacultyRepository>()
                .InstancePerRequest();

            builder.RegisterType<ReviewRepository>()
                .As<IReviewRepository>()
                .InstancePerRequest();

            builder.RegisterType<ProgrammeRepository>()
                .As<IProgrammeRepository>()
                .InstancePerRequest();

            builder.RegisterType<MapsEmbedApi>()
                .As<IMapsApi>()
                .InstancePerRequest();

            builder.RegisterType<PaginationHandler<University, UniversitySortOrder>>()
                .As<IPaginationHandler<University, UniversitySortOrder>>()
                .InstancePerRequest();

            builder.RegisterType<PaginationHandler<Review, ReviewSortOrder>>()
                .As<IPaginationHandler<Review, ReviewSortOrder>>()
                .InstancePerRequest();

            builder.RegisterType<ApplicationUserManager>()
                .AsSelf()
                .InstancePerRequest();

            builder.RegisterType<ApplicationSignInManager>()
                .AsSelf()
                .InstancePerRequest();

            builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication)
                .InstancePerRequest();

            builder.Register<IDataProtectionProvider>(c => app.GetDataProtectionProvider())
                .InstancePerRequest();

            builder.RegisterType<RoleStore<IdentityRole>>()
                .As<IRoleStore<IdentityRole, string>>()
                .InstancePerRequest();

            builder.RegisterType<RoleManager<IdentityRole>>()
                .AsSelf()
                .InstancePerRequest();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();

            InitializePaginationData();

            ConfigureAuth(app);
        }

        private void InitializePaginationData()
        {
            string path = ConfigurationManager.AppSettings["Environment"].ToString() == "Production"
                ? AppDomain.CurrentDomain.BaseDirectory + "bin\\realCountriesShort.txt"
                : AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("UniversityAdvisor2019") + 22)
                + "WebScraper\\realCountriesShort.txt";

            CountryStore.ReadCountryNamesFromFile(path);
        }
    }
}