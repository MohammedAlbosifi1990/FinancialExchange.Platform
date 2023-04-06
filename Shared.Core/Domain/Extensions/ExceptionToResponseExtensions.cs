using Microsoft.AspNetCore.Http;
using Shared.Core.Domain.Exceptions;
using Shared.Core.Domain.Models;

namespace Shared.Core.Domain.Extensions;

public static class ExceptionToResponseExtensions
{
    public static ApiResponse<object> ToResponse(this BaseException exception)
    {
        return exception.StatusCode switch
        {
            StatusCodes.Status404NotFound => ApiResponse.NotFound(exception.Message),
            StatusCodes.Status403Forbidden => ApiResponse.Forbidden(exception.Message),
            StatusCodes.Status409Conflict => ApiResponse.Conflict(exception.Message),
            StatusCodes.Status400BadRequest => ApiResponse.BadRequest(exception.Message),
            StatusCodes.Status401Unauthorized => ApiResponse.Unauthorized(exception.Message),
            StatusCodes.Status302Found => ApiResponse.Found(message:exception.Message),
            StatusCodes.Status500InternalServerError => ApiResponse.InternalServerError(exception.Message),
            _ => ApiResponse.InternalServerError("حدث خطأ أثناء معالجة الطلب ")
        };
    }
}