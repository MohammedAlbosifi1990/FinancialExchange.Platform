using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using Shared.Core.Contract.Services.Sms;
using Shared.Core.Localizations;
using Shared.Core.Services.Emails;

namespace Web.Api.Installers;

public static class LocalizerInstaller
{
    public static IServiceCollection AddLocalizer(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new[]
            {
                new CultureInfo("ar"),
                new CultureInfo("en")
            };

            options.DefaultRequestCulture = new RequestCulture(configuration["SystemCulture"] ?? "ar");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });

        services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
        services.AddSingleton<IStringLocalizer, JsonStringLocalizer>();
        services.AddLocalization(options => options.ResourcesPath = "Resources");

        
        return services;
    }

    public static WebApplication UseLocalizer(this WebApplication app)
    {
        app.UseRequestLocalization();
        return app;
    }   
}