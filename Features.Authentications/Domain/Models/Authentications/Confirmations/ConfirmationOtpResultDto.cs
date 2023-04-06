//  Project ReservationSystem
//  Created By Mohammed Albosifi At 07-2022
//  Private Email :MohammedAlbosifi1990@gmail.com
//  Phone : +218928574270

namespace Features.Authentications.Domain.Models.Authentications.Confirmations;

public interface IApiResult
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public dynamic? Data { get; set; }
}


public class ConfirmationOtpResultDto  : IApiResult
{
    public ConfirmationOtpResultDto(bool isSuccess=false,string message="")
    {
        IsSuccess = isSuccess;
        Message = message;
    }
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public dynamic? Data { get; set; }

    public static ConfirmationOtpResultDto Success()
    {
        return new ConfirmationOtpResultDto()
        {
            Message = null,
            IsSuccess = true
        };
    }
    
    public static ConfirmationOtpResultDto Field(string message)
    {
        return new ConfirmationOtpResultDto()
        {
            Message = message,
            IsSuccess = false
        };
    }
}