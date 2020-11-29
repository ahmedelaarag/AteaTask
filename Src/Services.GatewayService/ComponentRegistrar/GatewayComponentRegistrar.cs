using Unity;

namespace Services.GatewayService.ComponentRegistrar
{
    public class GatewayComponentRegistrar
    {
        public static void RegisterComponents(IUnityContainer container)
        {
            container.RegisterType<IGatewayService, GatewayService>();
        }
    }
}