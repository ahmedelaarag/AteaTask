using System.Net;

namespace Core.Interfaces
{
    public interface IResult
    {
        HttpStatusCode StatusCode { get; }
        ApiError Error { get; }
    }
    
    public interface IServiceResult<out T> : IResult where T : class
    {
        T Dto { get; }
    }
}