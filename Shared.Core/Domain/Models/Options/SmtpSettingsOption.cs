
namespace Shared.Core.Domain.Models.Options;

public class SmtpSettingsOption
{
    public required string SmtpServer { get; set; }
    public required int Port { get; set; }
    public required string SenderName { get; set; }
    public required string SenderEmail { get; set; }
    public required string SenderPassword { get; set; }
}