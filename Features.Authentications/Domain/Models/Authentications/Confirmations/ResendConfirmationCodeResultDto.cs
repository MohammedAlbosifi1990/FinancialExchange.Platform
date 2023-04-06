//  Project ReservationSystem
//  Created By Mohammed Albosifi At 07-2022
//  Private Email :MohammedAlbosifi1990@gmail.com
//  Phone : +218928574270

namespace Features.Authentications.Domain.Models.Authentications.Confirmations;

public class ResendConfirmationCodeResultDto
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public string? Code { get; set; }

    public static ResendConfirmationCodeResultDto Success(string code,string? message=null)
    {
        return new ResendConfirmationCodeResultDto()
        {
            Message = message,
            IsSuccess = true,
            Code = code
        };
    }
    
    public static ResendConfirmationCodeResultDto Field(string message)
    {
        return new ResendConfirmationCodeResultDto()
        {
            Message = message,
            IsSuccess = false
        };
    }
}