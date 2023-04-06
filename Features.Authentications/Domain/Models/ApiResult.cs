
namespace Features.Authentications.Domain.Models;

public class ApiResult
{
    public ApiResult(bool isSuccess=false,string message="")
    {
        IsSuccess = isSuccess;
        Message = message;
    }
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }


    public static ApiResult Result(string? message=null,bool isSuccess=true)
    {
        return new ApiResult()
        {
            Message = message,
            IsSuccess = isSuccess
        };
    }
}