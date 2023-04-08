using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Shared.Core.Contract.Services.Sms;
using Shared.Core.Domain.Models;
using Shared.Core.Domain.Models.Options;
using Shared.Core.Services.Emails;
using Shared.Core.Services.PushNotification;

namespace Web.Api.Installers;

public static class ServicesInstaller
{
    public static IServiceCollection AddServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<List<PlatformMinimalVersionModel>>(configuration.GetSection("PlatformsMinimalVersions"));
        
        
        services.Configure<PushNotification.FcmNotificationSetting>(configuration.GetSection("FcmNotification"));
        services.AddTransient<IPushNotification, PushNotification>();
        
        services.Configure<SmtpSettingsOption>(configuration.GetSection("SmtpSettings"));
        services.AddTransient<IEmailSender, EmailSender>();
        
        services.AddTransient<ISmsSender, SmsSender>();
        
        return services;
    }
}