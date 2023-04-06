using Microsoft.AspNetCore.Identity;
using Shared.Core.Domain.Enum;

namespace Shared.Core.Domain.Entities;

public class User : IdentityUser<Guid>, IBaseEntity
{
    public string FullName { get; set; } = null!;
    public string HashPass { get; set; } = null!;
    public string? ImagePath { get; set; }
    public bool IsAccepted { get; set; }
    public bool IsDisabled { get; set; }
    public UserType Type { get; set; } = UserType.User;

    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdate { get; set; }

    public ICollection<Permission>? Permissions { get; set; }

    #region Confirmation Code

    public string? ConfirmationCode { get; set; }
    public DateTime? ConfirmationCodeCreatedAt { get; set; }
    public ConfirmationCodeFor? ConfirmationCodeUsedFor { get; set; } = ConfirmationCodeFor.Phone;
    
    public string SetConfirmationCode(ConfirmationCodeFor confirmationCodeFor=ConfirmationCodeFor.Email, string? code = null )
    {
        ConfirmationCode = string.IsNullOrEmpty(code) ? Guid.NewGuid().ToString()[..7].ToUpper() : code;
        ConfirmationCodeCreatedAt = DateTime.UtcNow;
        ConfirmationCodeUsedFor = confirmationCodeFor;
        return ConfirmationCode;
    }

    public bool IsValidConfirmationCode(string code,out string error,int second=180)
    {
        if (DateTime.UtcNow > ConfirmationCodeCreatedAt!.Value.AddSeconds(second))
        {
            error = "CONFIRMATION_CODE_TIME_IS_SPEND";
            return false;
        }

        if (code != ConfirmationCode)
        {
            error = "CONFIRMATION_CODE_IS_NOT_EQUAL";
            return false;
        }

        error = "";
        RestConfirmationCode();
        return true;
    }
    public void RestConfirmationCode()
    {
        ConfirmationCode = null;
        ConfirmationCodeUsedFor = ConfirmationCodeFor.None;
        ConfirmationCodeCreatedAt = null;
    }

    #endregion
    #region Refresh Token

    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }

    public bool IsValidRefreshToken(string oldRefreshToken)
        => RefreshToken != oldRefreshToken || RefreshTokenExpiryTime <= DateTime.Now;

    public void ClearRefreshToken()
    {
        RefreshToken = null;
        RefreshTokenExpiryTime = null;
    } 

    #endregion

}