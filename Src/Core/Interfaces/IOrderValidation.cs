using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IOrderValidation
    {
        //Simple validation that validate the input data and return error message
        //Collection because we may return more than one error.
        IEnumerable<ApiError> ValidateOrderNumber(string orderNumber);
        IEnumerable<ApiError> ValidateUser(int userId);
        IEnumerable<ApiError> ValidateAmount(decimal amount);
    }
}