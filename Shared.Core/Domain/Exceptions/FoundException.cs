using Microsoft.AspNetCore.Http;

namespace Shared.Core.Domain.Exceptions;

public class FoundException : BaseException
{
    public FoundException(string message) : base(message,StatusCodes.Status302Found)
    {
    }
    
    public static FoundException Throw(string message)
    {
        return new FoundException(message);
    } 
}