using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using Webserver.Data;

namespace Webserver
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<ApplicationDbContext, ApplicationDbContext>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}