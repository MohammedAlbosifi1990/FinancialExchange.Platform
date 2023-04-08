
namespace Features.Authentications.Domain.Models.Authentications.Password;

public class ChangePasswordRequestDto
{
    public string Password { get; set; }
    public string OldPassword { get; set; }
}