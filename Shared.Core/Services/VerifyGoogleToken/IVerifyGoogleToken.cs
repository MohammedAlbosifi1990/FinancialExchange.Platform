using Shared.Core.Domain.Models;

namespace Shared.Core.Services.VerifyGoogleToken;

public interface IVerifyGoogleToken
{
    Task<ApiResult<GooglePayload>> Verify(string tokenId);
}