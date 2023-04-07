using System.Security.Claims;
using static System.Guid;

namespace Shared.Core.Domain.Extensions;

public static class ClaimsIdentityExtensions
{
    public static bool Id(this ClaimsPrincipal? claimsIdentity, out string? id)
        => TryGet(claimsIdentity, Domain.Constants.Constants.Claims.UserId, out id);


    public static bool Id(this ClaimsPrincipal? claimsIdentity, out Guid id)
    {
        if (claimsIdentity != null)
        {
            var claim = claimsIdentity.FindFirst(Domain.Constants.Constants.Claims.UserId);
            if (claim != null)
                return TryParse(claim.Value, out id);

            id = Empty;
            return false;
        }

        id = Empty;
        return false;
    }


    public static Guid UserId(this ClaimsPrincipal? claimsIdentity)
    {
        var id = claimsIdentity?.FindFirst(Constants.Constants.Claims.UserId);
        if (id == null) return Empty;

        return TryParse(id.Value, out var userId) ? userId : Empty;
    }

    public static bool Ip(this ClaimsPrincipal? claimsIdentity, out string? ip)
        => TryGet(claimsIdentity, "ip", out ip);

    public static bool TryGet(this ClaimsPrincipal? claimsIdentity, string key, out string? value)
    {
        if (claimsIdentity != null)
        {
            var claim = claimsIdentity.FindFirst(key);
            if (claim != null)
            {
                value = claim.Value;
                return true;
            }

            value = null;
            return false;
        }

        value = null;
        return false;
    }
}