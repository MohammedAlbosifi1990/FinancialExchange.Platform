
namespace Features.Authentications.Domain.Models.Authentications.SignIn;

public class SignInResponseDto
{
    public Guid? UserId { get; set; }
    public string? Name { get; set; }
    public string? ImagePath { get; set; }

    public string? AccessToken { get; set; }
    public DateTime? ExpiredAt { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }

    public bool IsDisabled { get; set; }
    public bool IsAccepted { get; set; }
}