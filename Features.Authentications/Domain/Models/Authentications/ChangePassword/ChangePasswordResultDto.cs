
using Features.Authentications.Domain.Models.Authentications.Confirmations;

namespace Features.Authentications.Domain.Models.Authentications.ChangePassword;

public class ChangePasswordResultDto
{
    public ChangePasswordResultDto(bool isSuccess=false,string message="")
    {
        IsSuccess = isSuccess;
        Message = message;
    }
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }


    public static ChangePasswordResultDto Result(string? message=null,bool isSuccess=true)
    {
        return new ChangePasswordResultDto()
        {
            Message = message,
            IsSuccess = false
        };
    }
}