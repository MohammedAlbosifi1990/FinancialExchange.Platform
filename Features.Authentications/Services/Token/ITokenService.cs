using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Features.Authentications.Domain.Models.Authentications.RefreshToken;
using Shared.Core.Domain.Entities;

namespace Features.Authentications.Services.Token;

public interface ITokenService
{
    Task<JwtSecurityToken> GenerateAccessToken(User user);
    Task<RefreshTokenResult> RefreshToken(string accessToken,string refreshToken);
    Task<RefreshTokenResult> RevokeToken(Guid userId);
    Task<ClaimsPrincipal?> GetPrincipalFromExpiredToken(string toke);
    Task<string> GenerateRefreshToken();

}   