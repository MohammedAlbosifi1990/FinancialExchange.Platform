using Google.Apis.Auth;
using Shared.Core.Domain.Models;

namespace Shared.Core.Services.VerifyGoogleToken;

public class VerifyGoogleToken : IVerifyGoogleToken
{
    public async Task<ApiResult<GooglePayload>> Verify(string tokenId)
    {
        try
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(tokenId);

            var gPayload = new GooglePayload()
            {
                Email = payload.Email,
                Locale = payload.Locale,
                Picture = payload.Picture,
                EmailVerified = payload.EmailVerified,
                UserId = payload.Subject
            };
            return ApiResult<GooglePayload>.Success(gPayload);
        }
        catch (InvalidJwtException e)
        {
            return ApiResult<GooglePayload>.Failed(e.Message);
        }
    }
}

public class GooglePayload 
{
    public string? UserId { get; set; }
    public string? Email { get; set; }
    public bool EmailVerified { get; set; }
    public string? Picture { get; set; }
    public string? Locale { get; set; }
}