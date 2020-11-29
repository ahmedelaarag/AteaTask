using System.Net;
using Core.Dto;
using Core.Interfaces;

namespace Core
{
    internal class Bank : IGateway
    {
        public IGatewayResult Process(OrderDto order)
        {
            //Connect to bank api to process the payment.
            //calling to third party api will return object result that could be mapped to our custom return type.
            return new GatewayResult(HttpStatusCode.OK);
        }
    }
}