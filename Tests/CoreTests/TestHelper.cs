using Core.Dto;
using Core.Enums;

namespace CoreTests
{
    public static class TestHelper
    {
        public static OrderDto CreateOrder(Gateway gateway, decimal amount = 1, string orderNumber = null, int userId = 1)
        {
            return new OrderDto
            {
                Amount = amount,
                OrderNumber = orderNumber ?? "AC-100",
                PaymentGateway = gateway,
                UserId = userId,
                Description = string.Empty
            };
        }
    }
}