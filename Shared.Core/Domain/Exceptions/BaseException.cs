using Microsoft.AspNetCore.Http;

namespace Shared.Core.Domain.Exceptions;

public class BaseException : Exception
{
    public int StatusCode { get; protected init; }

    protected BaseException(string message,int statusCode=StatusCodes.Status500InternalServerError) : base(message)
    {
        StatusCode = statusCode;
    }
}