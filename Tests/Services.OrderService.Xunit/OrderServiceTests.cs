using System.Net;
using System.Threading.Tasks;
using Core;
using Core.Dto;
using Core.Enums;
using Core.Xunit;
using NSubstitute;
using Services.GatewayService;
using Xunit;

namespace Services.OrderService.Xunit
{
    public class OrderServiceTests
    {
        private readonly IGatewayService _gatewayService;
        private readonly IOrderService _orderService;
        private readonly IOrderValidation _orderValidation;
        public OrderServiceTests()
        {
            _gatewayService = Substitute.For<IGatewayService>();
            _orderValidation = Substitute.For<IOrderValidation>();
            
            _orderService = new OrderService(_gatewayService, _orderValidation);
        }
        
        [Theory, InlineData(true), InlineData(false)]
        public async void ProcessOrderTests(bool successfulProcess)
        {
            await Task.Run(() =>
            {
                var order = TestHelper.CreateOrder(Gateway.Bank);

                if (successfulProcess)
                {
                    _gatewayService.ProcessInternalAsync(order)
                        .Returns(new ServiceResult<OrderDto>(order, null, HttpStatusCode.OK));
                }
                else
                {
                    _gatewayService.ProcessInternalAsync(order)
                        .Returns(new ServiceResult<OrderDto>(new ApiError("Error."), HttpStatusCode.InternalServerError));
                }

                var result = _orderService.ProcessOrder(order).Result;
                Assert.NotNull(result);
                if (successfulProcess)
                {
                    Assert.NotNull(result.Dto);
                    Assert.Equal(HttpStatusCode.OK, result.StatusCode);
                    Assert.Null(result.Error);
                }
                else
                {
                    Assert.Null(result.Dto);
                    Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
                    Assert.NotNull(result.Error);
                }
            });
        }
    }
}