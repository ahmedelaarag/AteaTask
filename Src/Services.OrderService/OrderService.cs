using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Core;
using Core.Dto;
using Services.GatewayService;

namespace Services.OrderService
{
    public interface IOrderService
    {
        Task<IServiceResult<PaymentReceiptDto>> ProcessOrder(OrderDto order);
    }
    
    internal class OrderService : IOrderService
    {
        private readonly IGatewayService _gatewayService;
        private readonly IOrderValidation _orderValidation;
        
        public OrderService(IGatewayService gatewayService, IOrderValidation orderValidation)
        {
            _gatewayService = gatewayService;
            _orderValidation = orderValidation;
        }

        public async Task<IServiceResult<PaymentReceiptDto>> ProcessOrder(OrderDto order)
        {
            try
            {
                var apiError = Validate(order);
                if (apiError.Errors.Any())
                {
                    return new ServiceResult<PaymentReceiptDto>(apiError, HttpStatusCode.BadRequest);
                }
                    
                var gatewayResponseResult = await _gatewayService.ProcessInternalAsync(order);
                return Map(gatewayResponseResult);
            }
            catch (NotImplementedException e)
            {
                return new ServiceResult<PaymentReceiptDto>(new ApiError(e.Message), HttpStatusCode.InternalServerError);
            }
        }

        private ApiError Validate(OrderDto orderDto)
        {
            var errors = new List<ApiError>();
            errors.AddRange(_orderValidation.ValidateOrderNumber(orderDto.OrderNumber));
            errors.AddRange(_orderValidation.ValidateUser(orderDto.UserId));
            errors.AddRange(_orderValidation.ValidateAmount(orderDto.Amount));
            errors.AddRange(_orderValidation.ValidateGatewayIdentifier(orderDto.PaymentGateway));
            
            return new ApiError(errors.FirstOrDefault()?.Message, errors);
        }

        /// <summary>
        /// Map IServiceResult<OrderDto> into IServiceResult<PaymentReceiptDto>
        /// and return it in respone
        /// </summary>
        private static IServiceResult<PaymentReceiptDto> Map(IServiceResult<OrderDto> gatewayResponseResult)
        {
            PaymentReceiptDto dto = null;
            if (gatewayResponseResult.StatusCode == HttpStatusCode.OK)
            {
                dto = new PaymentReceiptDto()
                {
                    Amount = gatewayResponseResult.Dto.Amount,
                    Description = gatewayResponseResult.Dto.Description,
                    OrderNumber = gatewayResponseResult.Dto.OrderNumber,
                    PaymentDate = DateTime.UtcNow,
                    UserId = gatewayResponseResult.Dto.UserId
                };
            }
            
            return new ServiceResult<PaymentReceiptDto>(dto, gatewayResponseResult.Error, gatewayResponseResult.StatusCode);
        }
    }
}