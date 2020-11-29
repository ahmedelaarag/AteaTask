using Core.Dto;

namespace Core.Interfaces
{
    public interface IGateway
    {
        //Each gateway will return different response,
        //the response shall be mapped into custom object.
        //since i am not connecting to real 3rd party abi i imagine a simple custom object
        IGatewayResult Process(OrderDto order);
    }
}