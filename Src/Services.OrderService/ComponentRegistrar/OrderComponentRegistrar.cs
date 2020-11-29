using Unity;

namespace Services.OrderService.ComponentRegistrar
{
    public class OrderComponentRegistrar
    {
        public static void RegisterComponents(IUnityContainer container)
        {
            container.RegisterType<IOrderService, OrderService>();
            container.RegisterType<IOrderValidation, OrderValidation>();
        }
    }
}