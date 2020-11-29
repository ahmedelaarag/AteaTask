using System.Collections.Generic;
using System.Net;
using Core.Interfaces;

namespace Core
{
    public class ServiceResult<T> : IServiceResult<T> where T : class
    {
        public ServiceResult(ApiError apiError, HttpStatusCode statusCode)
            : this(null, apiError, statusCode)
        {
        }

        public ServiceResult(T dto, ApiError apiError, HttpStatusCode statusCode)
        {
            Dto = dto;
            StatusCode = statusCode;
            Error = apiError;
        }
        
        public T Dto { get; }
        public HttpStatusCode StatusCode { get; }
        public ApiError Error { get; }
    }
}