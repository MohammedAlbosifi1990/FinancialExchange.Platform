using Microsoft.AspNetCore.Http;

namespace Shared.Core.Domain.Exceptions;

public class UploadFileException : BaseException
{
    public UploadFileException(string message, int statusCode=StatusCodes.Status400BadRequest) : base(message,statusCode)
    {
    }
    public static UploadFileException Throw(string message,int statusCode=StatusCodes.Status400BadRequest)
    {
        return new UploadFileException(message);
    } 
}