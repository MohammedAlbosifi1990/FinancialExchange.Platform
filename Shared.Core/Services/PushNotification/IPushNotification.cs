namespace Shared.Core.Services.PushNotification;

public interface IPushNotification
{
    Task<bool> Push(string[] deviceTokens, string title, string body, object data);
}