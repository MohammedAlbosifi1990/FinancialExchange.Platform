namespace Features.Authentications.Domain.Models.Authentications.Password;

public class ResetPasswordRequestDto
{
    public string Password { get; set; }
    public string Code { get; set; }
}