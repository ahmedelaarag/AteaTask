using Core.Interfaces;
using Core.Services;
using Core.Validations;
using Unity;

namespace Core.ComponentRegistrar
{
    public static class ComponentRegistrar
    {
        public static void RegisterComponents(IUnityContainer container)
        {
            container.RegisterType<IOrderService, OrderService>();
            container.RegisterType<IGatewayService, GatewayService>();
            container.RegisterType<IOrderValidation, OrderValidation>();
        }
    }
}