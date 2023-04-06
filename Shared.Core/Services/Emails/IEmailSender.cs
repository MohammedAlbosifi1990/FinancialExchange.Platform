namespace Shared.Core.Services.Emails;

public interface IEmailSender
{
    Task<string?> SendEmail(string email, string message);
}