using System.Net;
using Core.Enums;
using Core.Xunit;
using Services.GatewayService.Gateways;
using Xunit;

namespace Services.GatewayService.Xunit
{
    public class BankTests
    {
        private readonly IGateway _gateway;

        public BankTests()
        {
            _gateway = new Bank();
        }

        [Fact]
        public void Process()
        {
            var order = TestHelper.CreateOrder(Gateway.Bank);
            var result = _gateway.ProcessAsync(order).Result;
            
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}