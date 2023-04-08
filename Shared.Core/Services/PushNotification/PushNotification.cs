using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Shared.Core.Services.PushNotification;

public class PushNotification : IPushNotification
{
    private readonly FcmNotificationSetting _fcmNotificationSetting;
    private static readonly Uri FireBasePushNotificationsUrl = new Uri("https://fcm.googleapis.com/fcm/send");

    public PushNotification(IOptions<FcmNotificationSetting> settings)
    {
        _fcmNotificationSetting = settings.Value;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="deviceTokens">List of all devices assigned to a user</param>
    /// <param name="title">Title of Notification</param>
    /// <param name="body">Description of Notification</param>
    /// <param name="data">Object with all extra information you want to send hidden in the Notification</param>
    /// <returns></returns>
    public async Task<bool> Push(string[] deviceTokens, string title, string body, object data)
    {
        var sent = false;

        if (!deviceTokens.Any()) return sent;

        var messageInformation = new Message()
        {
            Notification = new Notification()
            {
                Title = title,
                Text = body
            },
            Data = data,
            RegistrationIds = deviceTokens
        };

        var jsonMessage = JsonConvert.SerializeObject(messageInformation);

        /*
             ------ JSON STRUCTURE ------
             {
                Notification: {
                                Title: "",
                                Text: ""
                                },
                Data: {
                        action: "Play",
                        playerId: 5
                        },
                RegistrationIds = ["id1", "id2"]
             }
             ------ JSON STRUCTURE ------
             */

        //Create request to Firebase API
        var request = new HttpRequestMessage(HttpMethod.Post, FireBasePushNotificationsUrl);

        request.Headers.TryAddWithoutValidation("Authorization", "key=" + _fcmNotificationSetting.ServerKey);
        request.Content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");

        using var client = new HttpClient();
        var result = await client.SendAsync(request);
        sent = sent && result.IsSuccessStatusCode;

        return sent;
    }


    private class Message
    {
        public string[] RegistrationIds { get; set; } = null!;
        public Notification Notification { get; set; } = null!;
        public object Data { get; set; } = null!;
    }

    private class Notification
    {
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;
    }


    public class FcmNotificationSetting
    {
        public string SenderId { get; set; } = null!;
        public string ServerKey { get; set; } = null!;
    }
}