using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Core.Dto;
using Services.OrderService;

namespace Presentation.RestApi.Controller
{
    public class OrderController : ApiController
    {
        private readonly IOrderService _orderService;
        
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        
        [HttpPost]
        [ResponseType(typeof(PaymentReceiptDto))]
        public async Task<IHttpActionResult> Pay([FromBody]OrderDto order)
        {
            if (order == null) return BadRequest(nameof(order));

            var serviceResult = await _orderService.ProcessOrder(order);
            switch (serviceResult.StatusCode)
            {
                case HttpStatusCode.OK:
                    return Ok(serviceResult.Dto);
                default:
                    return Content(serviceResult.StatusCode, serviceResult.Error);
            }
        }
    }
}