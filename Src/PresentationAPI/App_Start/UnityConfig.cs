using System.Web.Http;
using Core.ComponentRegistrar;
using Unity;
using Unity.WebApi;

namespace PresentationAPI
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
            var container = new UnityContainer();

            ComponentRegistrar.RegisterComponents(container);
            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}