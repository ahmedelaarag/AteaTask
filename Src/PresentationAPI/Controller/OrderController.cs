using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Core.Dto;
using Core.Services;

namespace PresentationAPI
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
        public IHttpActionResult OrderPayment([FromBody]OrderDto order)
        {
            if (order == null) return BadRequest(nameof(order));

            var apiError = _orderService.Validate(order);
            if (apiError.Errors.Any())
            {
                return Content(HttpStatusCode.BadRequest, apiError);
            }
            
            var serviceResult = _orderService.ProcessOrder(order);
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