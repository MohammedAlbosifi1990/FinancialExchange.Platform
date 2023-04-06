
namespace Shared.Core.Domain.Exceptions;

public class ApiException : BaseException
{
    public ApiException(string message,int statusCode) : base(message,statusCode) {}

    public static ApiException Throw(string message, int statusCode)
    {
        return new ApiException(message, statusCode);
    } 
}