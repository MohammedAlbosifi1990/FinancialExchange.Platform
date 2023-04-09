using Microsoft.AspNetCore.Http;

namespace Shared.Core.Domain.Exceptions;

public class InvalidArgumentException : BaseException
{
    public InvalidArgumentException(string message) : base(message,StatusCodes.Status400BadRequest)
    {
    }
    public static InvalidArgumentException Throw(string message)
    {
        return new InvalidArgumentException(message);
    }
}

