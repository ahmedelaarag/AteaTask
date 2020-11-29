using System.Web.Http;
using Services.GatewayService.ComponentRegistrar;
using Services.OrderService.ComponentRegistrar;
using Unity;
using Unity.WebApi;

namespace Presentation.RestApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
            var container = new UnityContainer();

            GatewayComponentRegistrar.RegisterComponents(container);
            OrderComponentRegistrar.RegisterComponents(container);
            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}