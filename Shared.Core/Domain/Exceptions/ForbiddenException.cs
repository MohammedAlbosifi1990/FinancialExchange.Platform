using Microsoft.AspNetCore.Http;

namespace Shared.Core.Domain.Exceptions;

public class ForbiddenException : BaseException
{
    public ForbiddenException(string message) : base(message,StatusCodes.Status403Forbidden)
    {
    }
    
    public static ForbiddenException Throw(string message)
    {
        return new ForbiddenException(message);
    } 
}