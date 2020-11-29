using System;
using Core;
using Core.Enums;
using Xunit;

namespace CoreTests.Gateways
{
    public class GatewayFactoryTests
    {
        [Theory]
        [InlineData(Gateway.Bank, true)]
        [InlineData(Gateway.PayPal, true)]
        [InlineData(Gateway.None, false)]
        [InlineData(null, false)]
        public void ResolveGatewayTests(Gateway identifier, bool success)
        {
            if (!success)
            {
                Assert.Throws<NotImplementedException>(() => GatewayFactory.ResolveGateway(identifier));
            }
            else
            {
                var gateway = GatewayFactory.ResolveGateway(identifier);
                Assert.NotNull(gateway);
            }
        }
    }
}