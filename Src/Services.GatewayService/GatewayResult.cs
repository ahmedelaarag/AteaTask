using System.Net;
using Core;

namespace Services.GatewayService
{
    internal class GatewayResult : IGatewayResult
    {
        public GatewayResult(HttpStatusCode statusCode) 
            : this(null, statusCode)
        {
        }
        public GatewayResult(ApiError error, HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
            Error = error;
        }
        
        public HttpStatusCode StatusCode { get; }
        public ApiError Error { get; }
    }
}