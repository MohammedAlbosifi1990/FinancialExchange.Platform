namespace Shared.Core.Services.PushNotification;

public interface IPushNotification
{
    Task<ResponseModel> Push(NotificationModel notification);
}