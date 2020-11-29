using System.Net;
using Core.Dto;
using Core.Interfaces;
using IServiceResult = Core.Interfaces.IServiceResult<Core.Dto.OrderDto>;

namespace Core.Services
{
    public interface IGatewayService
    {
        IServiceResult ProcessInternal(OrderDto order);
    }
    
    internal class GatewayService : IGatewayService
    {
        public IServiceResult ProcessInternal(OrderDto order)
        {
            var gateway = GatewayFactory.ResolveGateway(order.PaymentGateway);
            if (gateway == null)
            {
                return new ServiceResult<OrderDto>(new ApiError($"{nameof(order.PaymentGateway)} is not yet implemented"), HttpStatusCode.BadGateway);
            }
            
            var result = gateway.Process(order);
            return Map(result, order);
        }

        /// <summary>
        /// Map the result from 3rd party call into IServiceResult<OrderDto>
        /// </summary>
        private static IServiceResult Map(IGatewayResult gatewayResult, OrderDto order)
        {
            return new ServiceResult<OrderDto>(order, gatewayResult.Error, gatewayResult.StatusCode);
        }
    }
}