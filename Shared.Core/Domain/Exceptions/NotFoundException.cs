using Microsoft.AspNetCore.Http;

namespace Shared.Core.Domain.Exceptions;

public class NotFoundException : BaseException
{
    public NotFoundException(string message) : base(message,StatusCodes.Status404NotFound)
    {
    }
    public static NotFoundException Throw(string message)
    {
        return new NotFoundException(message);
    } 
}