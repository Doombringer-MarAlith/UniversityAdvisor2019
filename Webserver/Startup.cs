using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin;
using Owin;
using System.Web;
using System.Web.Mvc;
using Webserver.Data;
using Webserver.Data.Infrastructure;
using Webserver.Data.Repositories;
using Webserver.Models;

[assembly: OwinStartupAttribute(typeof(Webserver.Startup))]
namespace Webserver
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ApplicationDbContext>().AsSelf().SingleInstance().OnActivated(x => x.Instance.Initialize());
            builder.RegisterType<ApplicationUserRepository>().As<IUserStore<ApplicationUser>>().InstancePerRequest();
            builder.RegisterType<UniversityRepository>().As<IUniversityRepository>().InstancePerRequest();
            builder.RegisterType<FacultyRepository>().As<IFacultyRepository>().InstancePerRequest();
            builder.RegisterType<ReviewRepository>().As<IReviewRepository>().InstancePerRequest();
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            builder.Register<IDataProtectionProvider>(c => app.GetDataProtectionProvider()).InstancePerRequest();
            builder.RegisterType<RoleStore<IdentityRole>>().As<IRoleStore<IdentityRole, string>>().InstancePerRequest();
            builder.RegisterType<RoleManager<IdentityRole>>().AsSelf().InstancePerRequest();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            
            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();

            ConfigureAuth(app);
        }
    }
}
