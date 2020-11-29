using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Core.Dto;
using Core.Interfaces;
using IServiceResult = Core.Interfaces.IServiceResult<Core.Dto.PaymentReceiptDto>;

namespace Core.Services
{
    public interface IOrderService
    {
        IServiceResult ProcessOrder(OrderDto order);
        ApiError Validate(OrderDto orderDto);
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

        public IServiceResult ProcessOrder(OrderDto order)
        {
            try
            {
                var gatewayResponseResult = _gatewayService.ProcessInternal(order);
                return Map(gatewayResponseResult);
            }
            catch (NotImplementedException e)
            {
                return new ServiceResult<PaymentReceiptDto>(new ApiError(e.Message), HttpStatusCode.InternalServerError);
            }
        }

        public ApiError Validate(OrderDto orderDto)
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
        private static IServiceResult Map(IServiceResult<OrderDto> gatewayResponseResult)
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