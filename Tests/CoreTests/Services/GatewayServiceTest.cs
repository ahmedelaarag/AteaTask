using System;
using System.Net;
using Core.Enums;
using Core.Services;
using Xunit;

namespace CoreTests.Services
{
    public class GatewayServiceTest
    {
        private readonly IGatewayService _gatewayService;

        public GatewayServiceTest()
        {
            _gatewayService = new GatewayService();
        }
        
        [Theory]
        [InlineData(Gateway.Bank, true)]
        [InlineData(Gateway.PayPal, true)]
        [InlineData(Gateway.None, false)]
        [InlineData(null, false)]
        public void ProcessInternalTests(Gateway paymentGateway, bool success)
        {
            var order = TestHelper.CreateOrder(paymentGateway);

            if (!success)
            {
                Assert.Throws<NotImplementedException>(() => _gatewayService.ProcessInternal(order));
            }
            else
            {
                var result = _gatewayService.ProcessInternal(order);
                Assert.NotNull(result);
                Assert.NotNull(result.Dto);
                Assert.Equal(HttpStatusCode.OK, result.StatusCode);
                Assert.Null(result.Error);
            }
        }
    }
}