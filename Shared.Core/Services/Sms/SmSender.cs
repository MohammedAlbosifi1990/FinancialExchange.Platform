namespace Shared.Core.Contract.Services.Sms;

public class SmsSender : ISmsSender
{

    public async Task<string?> SendSms(string phone, string message)
    {
        return null;
    }
}