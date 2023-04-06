using Microsoft.AspNetCore.Http;

namespace Shared.Core.Domain.Exceptions;

public class UnauthorizedException : BaseException
{
    public UnauthorizedException(string message) : base(message,StatusCodes.Status401Unauthorized)
    {
    }
    
    public static UnauthorizedException Throw(string message)
    {
        return new UnauthorizedException(message);
    } 
}