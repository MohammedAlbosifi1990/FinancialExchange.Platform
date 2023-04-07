
namespace Shared.Core.Domain.Models.Options;

public class AuthenticationsOption
{
    public int AllowedPeriodForReLoginIn { get; set; }
    public int NumberOfTimesAllowedToLogIn { get; set; }
    public int ConfirmationCodeExpirationTimeInMinute { get; set; }
    public required string HashPassKey { get; set; }
    public int LockoutTimeInMinute { get; set; }
    public int LockoutCount { get; set; } = 5;
    public bool AutoAcceptable { get; set; }
    public JWTOptions JwtOptions { get; set; }
}

public class JWTOptions
{
    public string ValidAudience { get; set; } = null!;
    public string ValidIssuer { get; set; } = null!;
    public string Secret { get; set; } = null!;
    public int TokenValidityInMin { get; set; }
    public int RefreshTokenValidityInDays { get; set; }
}