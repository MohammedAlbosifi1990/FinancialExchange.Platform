
namespace Features.Authentications.Domain.Models.Authentications.RefreshToken;

public class RefreshTokenResult
{
    public RefreshTokenResult(bool isSuccess=false,string message="")
    {
        IsSuccess = isSuccess;
        Message = message;
    }
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    
    public string AccessToken { get; set; } = null!;
    public DateTime ExpiredAt { get; set; } 
    public string RefreshToken { get; set; } = null!;

    public static RefreshTokenResult SetAsSuccess(string accessToken,string refreshToken,DateTime expiredAt)
    {
        return new RefreshTokenResult()
        {
            Message = null,
            IsSuccess = true,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiredAt = expiredAt
        };
    }
    
    public static RefreshTokenResult SetAsSuccess()
    {
        return new RefreshTokenResult()
        {
            Message = null,
            IsSuccess = true
        };
    }
    
    public static RefreshTokenResult SetError(string message)
    {
        return new RefreshTokenResult()
        {
            Message = message,
            IsSuccess = false
        };
    }
}