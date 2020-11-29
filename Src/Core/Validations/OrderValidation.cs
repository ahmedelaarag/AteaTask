using System.Collections.Generic;
using Core.Interfaces;

namespace Core.Validations
{
    internal class OrderValidation : IOrderValidation
    {
        public IEnumerable<ApiError> ValidateOrderNumber(string orderNumber)
        {
            if (string.IsNullOrEmpty(orderNumber))
            {
                yield return new ApiError("Order number can not be null or empty");
            }
        }

        public IEnumerable<ApiError> ValidateUser(int userId)
        {
            //Just will check if we have id or not. for real system can validate against repo.
            if (userId <= 0)
            {
                yield return new ApiError("Wrong user id.");
            }
        }

        public IEnumerable<ApiError> ValidateAmount(decimal amount)
        {
            if (amount <= decimal.Zero)
            {
                yield return new ApiError("Amount must be greater than zero");
            }
        }
    }
}