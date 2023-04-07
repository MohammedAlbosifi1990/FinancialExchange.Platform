using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Features.Authentications.Domain.Models.Authentications.RefreshToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Core.Domain.Constants;
using Shared.Core.Domain.Entities;
using Shared.Core.Domain.Extensions;
using Shared.Core.Domain.Models.Options;

namespace Features.Authentications.Services.Token;

public class TokenService : ITokenService
{
    private readonly IStringLocalizer _localizer;
    private readonly UserManager<User> _userManager;
    private readonly AuthenticationsOption _authenticationsOption;

    public TokenService(
        UserManager<User> userManager,
        IOptions<AuthenticationsOption> authenticationsOption,
        IStringLocalizer localizer)
    {
        _userManager = userManager;
        _authenticationsOption = authenticationsOption.Value;
        _localizer = localizer;
    }

    public async Task<JwtSecurityToken> GenerateAccessToken(User user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);

        var userClaims = await _userManager.GetClaimsAsync(user);
        var authClaims = new List<Claim>
        {
            new("UserId", user.Id.ToString()),
            new("Name", user.FullName),
            new("Type", user.Type.ToString()),
            new("IsDisabled", user.IsDisabled.ToString()),
            new("IsAccepted", user.IsAccepted.ToString()),
            new("EmailConfirmed", user.EmailConfirmed.ToString()),
            new("PhoneConfirmed", user.PhoneNumberConfirmed.ToString()),
        };

        if (!string.IsNullOrEmpty(user.UserName))
            authClaims.Add(new("UserName", user.UserName));

        if (!string.IsNullOrEmpty(user.Email))
            authClaims.Add(new("UserName", user.Email));

        if (!string.IsNullOrEmpty(user.PhoneNumber))
            authClaims.Add(new("Phone", user.PhoneNumber));

        authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));
        authClaims.AddRange(userClaims);

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationsOption.JwtOptions.Secret));

        var token = new JwtSecurityToken(
            _authenticationsOption.JwtOptions.ValidIssuer,
            audience: _authenticationsOption.JwtOptions.ValidAudience,
            expires: DateTime.Now.AddDays(_authenticationsOption.JwtOptions.TokenValidityInMin),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );
        return token;
    }

    public async Task<RefreshTokenResult> RefreshToken(string accessToken, string refreshToken)
    {
        var principal = await GetPrincipalFromExpiredToken(accessToken);
        if (principal == null)
            return RefreshTokenResult.SetError(_localizer[AuthenticationsConst.InvalidAccessToken]);

        var id = principal.UserId();

        if (id == Guid.Empty)
            return RefreshTokenResult.SetError(_localizer[AuthenticationsConst.InvalidAccessToken]);

        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user == null || user.IsValidRefreshToken(refreshToken))
            return RefreshTokenResult.SetError(_localizer[AuthenticationsConst.InvalidRefreshToken]);

        var newAccessToken = await GenerateAccessToken(user);
        var newRefreshToken = await GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow;
        await _userManager.UpdateAsync(user);

        return RefreshTokenResult.SetAsSuccess(
            new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            newRefreshToken,
            newAccessToken.ValidTo);
    }

   

    public async Task<RefreshTokenResult> RevokeToken(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null) return RefreshTokenResult.SetError(_localizer[AuthenticationsConst.UserNotExist]);

        user.RefreshToken = null;
        user.RefreshTokenExpiryTime = null;
        await _userManager.UpdateAsync(user);

        return RefreshTokenResult.SetAsSuccess();
    }

    public  Task<ClaimsPrincipal?> GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationsOption.JwtOptions.Secret)),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
        {
            // throw new SecurityTokenException(_localizer[Localizations.InvalidAccessToken]);
            principal = null;
        }

        return Task.FromResult(principal);
        
    }

    public Task<string> GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Task.FromResult(Convert.ToBase64String(randomNumber));
    }
}