namespace Shared.Core.Contract.Services.Sms;

public interface ISmsSender
{
    Task<string?> SendSms(string phone, string message);
}