using Microsoft.AspNetCore.Http;

namespace Shared.Core.Domain.Exceptions;

public class PlatFormVersionException : BaseException
{
    public PlatFormVersionException(string message) : base(message,StatusCodes.Status403Forbidden)
    {
        StatusCode = StatusCodes.Status403Forbidden;
    }
    
    public static PlatFormVersionException Throw(string message)
    {
        return new PlatFormVersionException(message);
    } 
}