
namespace Features.Authentications.Domain.Models.Authentications.SignIn;

public class SignInResultDto 
{
    public string? Name { get; set; }
    public Guid? UserId { get; set; }

    public string? Token { get; set; } 
    public DateTime? ExpiredAt { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public bool IsAccepted { get; set; }
    public bool IsDisabled { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? ImagePath { get; set; }

    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    
    public static SignInResultDto Failed(string message="" )
    {
        return new SignInResultDto()
        {
            Message = message,
            IsSuccess = false
        };
    }
}