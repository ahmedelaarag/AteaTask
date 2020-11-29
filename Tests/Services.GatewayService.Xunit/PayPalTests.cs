using System.Net;
using Core.Enums;
using Core.Xunit;
using Services.GatewayService.Gateways;
using Xunit;

namespace Services.GatewayService.Xunit
{
    public class PayPalTests
    {
        private readonly IGateway _gateway;

        public PayPalTests()
        {
            _gateway = new PayPal();
        }

        [Fact]
        public void Process()
        {
            var order = TestHelper.CreateOrder(Gateway.PayPal);
            var result = _gateway.ProcessAsync(order).Result;
            
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}