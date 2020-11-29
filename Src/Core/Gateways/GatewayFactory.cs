﻿using System;
using Core.Enums;
using Core.Interfaces;

namespace Core
{
    internal static class GatewayFactory
    {
        public static IGateway ResolveGateway(Gateway identifier)
        {
            switch(identifier)
            {
                case Gateway.PayPal:
                    return new PayPal();

                case Gateway.Bank:
                    return new Bank();

                default:
                    throw new NotImplementedException($"{nameof(identifier)} is not implemented yet.");
            }
        }
    }
}