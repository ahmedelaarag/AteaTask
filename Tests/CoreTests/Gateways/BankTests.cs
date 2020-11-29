using System.Net;
using Core;
using Core.Enums;
using Core.Interfaces;
using Xunit;

namespace CoreTests.Gateways
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
            var result = _gateway.Process(order);
            
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}