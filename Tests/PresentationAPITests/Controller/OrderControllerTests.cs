﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http.Results;
using Core;
using Core.Dto;
using Core.Enums;
using Core.Services;
using Core.Validations;
using CoreTests;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using PresentationAPI;
using Xunit;

namespace PresentationAPITests.Controller
{
    public class OrderControllerTests
    {
        private readonly IGatewayService _gatewayService;
        private readonly IOrderService _orderService;
        private readonly OrderController _orderController;
        
        public OrderControllerTests()
        {
            _gatewayService = Substitute.For<IGatewayService>();
            _orderService = new OrderService(_gatewayService, new OrderValidation());
            _orderController = new OrderController(_orderService);
        }
        
        [Theory]
        [MemberData(nameof(GetOrders))]
        public void OrderPaymentTests(Gateway gateway, decimal amount, string orderNumber, int userId, bool success)
        {
           var order = TestHelper.CreateOrder(gateway, amount, orderNumber, userId);

           if (gateway == Gateway.None)
           {
               _gatewayService.ProcessInternal(order)
                   .Throws(new NotImplementedException($"{nameof(gateway)} is not implemented yet."));
           }
           else
           {
               _gatewayService.ProcessInternal(order)
                   .Returns(new ServiceResult<OrderDto>(order, null, HttpStatusCode.OK));
           }

           if (!success && gateway == Gateway.None)
           {
               Assert.Throws<NotImplementedException>(() => _orderController.OrderPayment(order));
           }
           else
           {
               var result = _orderController.OrderPayment(order);
               Assert.NotNull(result);
               if (!success)
               {
                   var errorResponse = result as NegotiatedContentResult<ApiError>;
                   Assert.NotNull(errorResponse);
                   Assert.NotNull(errorResponse.Content);
                   var apiError = errorResponse.Content;
                   Assert.Single(apiError.Errors);
                   Assert.NotNull(apiError.Message);
               }
               else
               {
                   var okResponse = result as OkNegotiatedContentResult<PaymentReceiptDto>;
                   Assert.NotNull(okResponse);
                   Assert.NotNull(okResponse.Content);
                   var paymentReceipt = okResponse.Content;
                   Assert.Equal(paymentReceipt.OrderNumber, order.OrderNumber);
                   Assert.Equal(paymentReceipt.UserId, order.UserId);
                   Assert.Equal(paymentReceipt.Amount, order.Amount);
               }
           }
        }
        
        public static IEnumerable<object[]> GetOrders()
        {
            return new[]
            {
                new object[]
                {
                    Gateway.Bank,
                    10,
                    "AC-111",
                    1,
                    true
                },
                new object[]
                {
                    Gateway.PayPal,
                    10,
                    "B-01",
                    1,
                    true
                },
                new object[]
                {
                    Gateway.None,
                    10,
                    "#0020",
                    1,
                    false
                },
                new object[]
                {
                    Gateway.Bank,
                    -10,
                    "10125",
                    1,
                    false
                },
                new object[]
                {
                    Gateway.PayPal,
                    10,
                    string.Empty,
                    1,
                    false
                },
                new object[]
                {
                    Gateway.PayPal,
                    10,
                    null,
                    0,
                    false
                },
                new object[]
                {
                    Gateway.PayPal,
                    1.5,
                    "#0001",
                    122,
                    true
                }
            };
        }
    }
}