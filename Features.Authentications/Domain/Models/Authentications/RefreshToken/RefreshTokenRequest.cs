
using System.ComponentModel.DataAnnotations;

namespace Features.Authentications.Domain.Models.Authentications.RefreshToken;

public class RefreshTokenRequest
{
    [Required]
    public string AccessToken { get; set; } = null!;
    [Required]
    public string RefreshToken { get; set; } = null!;
}