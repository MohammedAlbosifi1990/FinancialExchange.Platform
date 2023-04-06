
namespace Shared.Core.Domain.Models;

public class ApiResponse : ApiResponse<object>
{
}
public class ApiResponse<T>
{
    public T? Data { get; private init; }
    public bool IsSuccess { get; private init; }
    public string? Message { get; private init; }
    public string? StatusCode { get; private init; }

    
    public ApiResponse(T? data=default,bool isSuccess=false,string? message=null,string? statusCode="RESPONSE")
    {
        Data = data;
        IsSuccess = isSuccess;
        Message = message;
        StatusCode = statusCode;
    }
    public static ApiResponse<T> Ok(T? data=default,string? message=null,string? statusCode="OK")
    {
        return new ApiResponse<T>()
        {
            IsSuccess = true,
            StatusCode = statusCode,
            Message = message,
            Data = data
        };
    }
    
    public static ApiResponse<T> NotFound(string? message=null,string? statusCode="NOT_FOUND")
    {
        return new ApiResponse<T>()
        {
            IsSuccess = false,
            StatusCode = statusCode,
            Message = message
        };
    }
    public static ApiResponse<T> Forbidden(string? message=null,string? statusCode="Forbidden")
    {
        return new ApiResponse<T>()
        {
            IsSuccess = false,
            StatusCode = statusCode,
            Message = message
        };
    }
    public static ApiResponse<T> Conflict(string? message=null,string? statusCode="Conflict")
    {
        return new ApiResponse<T>()
        {
            IsSuccess = false,
            StatusCode = statusCode,
            Message = message
        };
    }
    
    
    public static ApiResponse<T> BadRequest(string? message=null,string? statusCode="BAD_REQUEST")
    {
        return new ApiResponse<T>()
        {
            IsSuccess = false,
            StatusCode = statusCode,
            Message = message
        };
    }
    public static ApiResponse<T> Unauthorized(string? message=null,string? statusCode="Unauthorized")
    {
        return new ApiResponse<T>()
        {
            IsSuccess = false,
            StatusCode = statusCode,
            Message = message
        };
    }
    
    public static ApiResponse<T> Found(T? data=default,string? message=null,string? statusCode="FOUND")
    {
        return new ApiResponse<T>()
        {
            IsSuccess = false,
            StatusCode = statusCode,
            Message = message,
            Data = data
        };
    }
    
    public static ApiResponse<T> Return(T? data=default,bool isSuccess=false,string? message=null,string? statusCode="RESPONSE")
    {
        return new ApiResponse<T>()
        {
            IsSuccess = isSuccess,
            StatusCode = statusCode,
            Message = message,
            Data = data
        };
    }
    
    public static ApiResponse<T> Created(T? data=default,bool isSuccess=true,string? message=null,string? statusCode="Created")
    {
        return new ApiResponse<T>()
        {
            IsSuccess = isSuccess,
            StatusCode = statusCode,
            Message = message,
            Data = data
        };
    }
    
    
    public static ApiResponse<T> InternalServerError(string? message=null,string? statusCode="InternalServerError_500")
    {
        return new ApiResponse<T>()
        {
            IsSuccess = false,
            StatusCode = statusCode,
            Message = message,
        };
    }
}
