using Microsoft.AspNetCore.Http;

namespace Shared.Core.Domain.Exceptions;

public class ConflictException : BaseException
{
    public ConflictException(string message) : base(message,StatusCodes.Status409Conflict)
    {
    }
    
    public static ConflictException Throw(string message)
    {
        return new ConflictException(message);
    } 
}