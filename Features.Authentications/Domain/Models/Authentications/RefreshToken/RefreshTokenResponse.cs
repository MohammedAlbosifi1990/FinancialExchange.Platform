﻿
namespace Features.Authentications.Domain.Models.Authentications.RefreshToken;

public class RefreshTokenResponse
{
    
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}