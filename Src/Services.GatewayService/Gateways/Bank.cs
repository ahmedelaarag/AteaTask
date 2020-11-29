using System.Net;
using System.Threading.Tasks;
using Core.Dto;

namespace Services.GatewayService.Gateways
{
    internal class Bank : IGateway
    {
        //request to 3rd party api.
        public async Task<IGatewayResult> ProcessAsync(OrderDto order)
        {
            return await Task.Factory.StartNew(() => new GatewayResult(HttpStatusCode.OK));
        }
    }
}