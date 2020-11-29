using System.Net;
using Core;
using Core.Enums;
using Core.Interfaces;
using Xunit;

namespace CoreTests.Gateways
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
            var result = _gateway.Process(order);
            
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}