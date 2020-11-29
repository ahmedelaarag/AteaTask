using System.Collections.Generic;
using Core;
using Core.Interfaces;
using Core.Validations;
using Xunit;

namespace CoreTests.Validations
{
    public class OrderValidationTests
    {
        private readonly IOrderValidation _orderValidation;

        public OrderValidationTests()
        {
            _orderValidation = new OrderValidation();
        }
        
        [Theory]
        [InlineData("0000", true)]
        [InlineData(null, false)]
        [InlineData("", false)]
        public void ValidateOrderNumber(string oderNumber, bool expectedResult)
        {
           var result = _orderValidation.ValidateOrderNumber(oderNumber);
           AssertResult(result, expectedResult);
        }
        
        [Theory, InlineData(0, false), InlineData(-1, false), InlineData(100, true)]
        public void ValidateUser(int userId, bool expectedResult)
        {
           var result = _orderValidation.ValidateUser(userId);
           AssertResult(result, expectedResult);
        }
        
        [Theory]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        [InlineData(100, true)]
        [InlineData(1.5, true)]
        [InlineData(0.5, true)]
        public void ValidateAmount(decimal amount, bool success)
        {
            var result = _orderValidation.ValidateAmount(amount);
            AssertResult(result, success);
        }

        private static void AssertResult(IEnumerable<ApiError> result, bool success)
        {
            Assert.NotNull(result);
            if (success)
            {
                Assert.Empty(result);
            }
            else
            {
                Assert.Single(result);
            }
        }
    }
}