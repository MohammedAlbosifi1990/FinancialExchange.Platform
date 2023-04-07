using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Shared.Core.Services.PushNotification;

public class PushNotification : IPushNotification
{
    private readonly FcmNotificationSetting _fcmNotificationSetting;

    public PushNotification(IOptions<FcmNotificationSetting> settings)
    {
        _fcmNotificationSetting = settings.Value;
    }


    public Task<ResponseModel> Push(NotificationModel notification)
    {
        var response = new ResponseModel();
        try
        {
            if (notification.IsAndroidDevice)
            {
                /* FCM Sender (Android Device) */
                // FcmSettings settings = new FcmSettings()
                // {
                // SenderId = _fcmNotificationSetting.SenderId,
                // ServerKey = _fcmNotificationSetting.ServerKey
                // };
                var httpClient = new HttpClient();

                var authorizationKey = $"key={_fcmNotificationSetting.ServerKey}";
                // var deviceToken = notificationModel.DeviceId;

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
                httpClient.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // var dataPayload = new GoogleNotification.DataPayload
                // {
                //     Title = notificationModel.Title,
                //     Body = notificationModel.Body
                // };

                // var notification = new GoogleNotification
                // {
                //     Data = dataPayload,
                //     Notification = dataPayload
                // };

                // var fcm = new FcmSender(settings, httpClient);
                // var fcmSendResponse = await fcm.SendAsync(deviceToken, notification);

                // if (fcmSendResponse.IsSuccess())
                // {
                // response.IsSuccess = true;
                // response.Message = "Notification sent successfully";
                // return response;
                // }
                // else
                // {
                // response.IsSuccess = false;
                // response.Message = fcmSendResponse.Results[0].Error;
                // return response;
                // }
            }
            // else
            // {
            /* Code here for APN Sender (iOS Device) */
            //var apn = new ApnSender(apnSettings, httpClient);
            //await apn.SendAsync(notification, deviceToken);
            // }

            return Task.FromResult(response);
        }
        catch (Exception)
        {
            response.IsSuccess = false;
            response.Message = "Something went wrong";
            return Task.FromResult(response);
        }
    }
}

public class ResponseModel
{
    [JsonProperty("isSuccess")] public bool IsSuccess { get; set; }
    [JsonProperty("message")] public string Message { get; set; } = null!;
}

public class NotificationModel
{
    [JsonProperty("deviceId")] public string DeviceId { get; set; } = null!;
    [JsonProperty("isAndroidDevice")] public bool IsAndroidDevice { get; set; }
    [JsonProperty("title")] public string Title { get; set; } = null!;
    [JsonProperty("body")] public string Body { get; set; } = null!;
}

public class GoogleNotification
{
    public class DataPayload
    {
        [JsonProperty("title")] public string Title { get; set; } = null!;
        [JsonProperty("body")] public string Body { get; set; } = null!;
    }

    [JsonProperty("priority")] public string Priority { get; set; } = "high";
    [JsonProperty("data")] public DataPayload Data { get; set; } = null!;
    [JsonProperty("notification")] public DataPayload Notification { get; set; } = null!;
}

public class FcmNotificationSetting
{
    public string SenderId { get; set; } = null!;
    public string ServerKey { get; set; } = null!;
}