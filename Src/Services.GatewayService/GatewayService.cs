using System.Net;
using System.Threading.Tasks;
using Core;
using Core.Dto;
using Services.GatewayService.Gateways;

namespace Services.GatewayService
{
    public interface IGatewayService
    {
        Task<ServiceResult<OrderDto>> ProcessInternalAsync(OrderDto order);
    }
    
    internal class GatewayService : IGatewayService
    {
        public async Task<ServiceResult<OrderDto>> ProcessInternalAsync(OrderDto order)
        {
            var gateway = GatewayFactory.ResolveGateway(order.PaymentGateway);
            if (gateway == null)
            {
                return new ServiceResult<OrderDto>(
                    new ApiError($"{nameof(order.PaymentGateway)} is not yet implemented"),
                    HttpStatusCode.BadGateway);

            }

            var result = await gateway.ProcessAsync(order);
            return Map(result, order);
        }

        /// <summary>
        /// Map the result from 3rd party call into IServiceResult<OrderDto>
        /// </summary>
        private static ServiceResult<OrderDto> Map(IGatewayResult gatewayResult, OrderDto order)
        {
            return new ServiceResult<OrderDto>(order, gatewayResult.Error, gatewayResult.StatusCode);
        }
    }
}