using Microsoft.AspNetCore.Http;

namespace Shared.Core.Domain.Models;

public class ApiResult
{
    public bool IsSuccess { get; protected init; } = true;
    public string? Message { get; protected init; }
    public int? StatusCode { get; set; } = StatusCodes.Status200OK;
    public static ApiResult Failed(string message, int? statusCode = StatusCodes.Status400BadRequest)
    {
        return new ApiResult()
        {
            IsSuccess = false,
            StatusCode = statusCode,
            Message = message
        };
    }
    
    
    public static ApiResult Success(string? message=null, int? statusCode = StatusCodes.Status200OK)
    {
        return new ApiResult()
        {
            IsSuccess = true,
            StatusCode = statusCode,
            Message = message
        };
    }
}
public class ApiResult<T> : ApiResult
{
    
    public T Data { get; private set; }
    
    
    public static ApiResult<T> Success(T data, string? message=null, int? statusCode = StatusCodes.Status200OK)
    {
        return new ApiResult<T>()
        {
            Data = data,
            IsSuccess = true,
            StatusCode = statusCode,
            Message = message
        };
    }
    public new static ApiResult<T> Failed(string message, int? statusCode = StatusCodes.Status400BadRequest)
    {
        return new ApiResult<T>()
        {
            IsSuccess = false,
            StatusCode = statusCode,
            Message = message
        };
    }
}