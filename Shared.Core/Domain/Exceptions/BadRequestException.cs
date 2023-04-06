using Microsoft.AspNetCore.Http;

namespace Shared.Core.Domain.Exceptions;

public class BadRequestException : BaseException
{
    public BadRequestException(string message) : base(message,StatusCodes.Status400BadRequest)
    {
    }
    public static BadRequestException Throw(string message)
    {
        return new BadRequestException(message);
    }
}

