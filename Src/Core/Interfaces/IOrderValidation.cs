using System.Collections.Generic;
using Core.Enums;

namespace Core.Interfaces
{
    public interface IOrderValidation
    {
        //Simple validation that validate the input data and return error message
        //Collection because we may return more than one error.
        IEnumerable<ApiError> ValidateOrderNumber(string orderNumber);
        IEnumerable<ApiError> ValidateGatewayIdentifier(Gateway gateway);
        IEnumerable<ApiError> ValidateUser(int userId);
        IEnumerable<ApiError> ValidateAmount(decimal amount);
    }
}