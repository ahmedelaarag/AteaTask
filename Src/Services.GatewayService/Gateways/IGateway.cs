using System.Threading.Tasks;
using Core.Dto;

namespace Services.GatewayService.Gateways
{
    public interface IGateway
    {
        //Each gateway will return different response,
        //the response shall be mapped into custom object.
        //since i am not connecting to real 3rd party abi i imagine a simple custom object
        Task<IGatewayResult> ProcessAsync(OrderDto order);
    }
}