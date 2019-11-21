using Microsoft.Owin;
using Owin;
using Webserver.Data;
using Webserver.Data.Repositories;
using Webserver.Models;
using Webserver.Data.Infrastructure;
using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

[assembly: OwinStartupAttribute(typeof(Webserver.Startup))]
namespace Webserver
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ApplicationDbContext>().AsSelf().SingleInstance();
            builder.RegisterType<ApplicationUserRepository>().As<IUserStore<ApplicationUser>>().InstancePerRequest();
            builder.RegisterType<UniversityRepository>().As<IUniversityRepository>().InstancePerRequest();
            builder.RegisterType<FacultyRepository>().As<IFacultyRepository>().InstancePerRequest();
            builder.RegisterType<ReviewRepository>().As<IReviewRepository>().InstancePerRequest();
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            builder.Register<IDataProtectionProvider>(c => app.GetDataProtectionProvider()).InstancePerRequest();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            
            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();

            ConfigureAuth(app);
        }
    }
}
