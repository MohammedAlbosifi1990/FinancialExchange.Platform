﻿using Microsoft.Extensions.FileProviders;
using Shared.Core;
using Shared.Core.Domain.Constants;
using Shared.DataPersistence;
using Web.Api.Middlewares;

namespace Web.Api.Installers;

public static class SystemInstaller
{
    public static IServiceCollection AddAllService(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddMemoryCache()
            .AddControllers(configuration)
            .AddSwagger(configuration)
            .AddDataPersistence(configuration)
            .AddFeatures(configuration)
            .AddValidations(configuration)
            .AddLocalizer(configuration)
            .AddServices(configuration);
        return services;
    }

    public static WebApplication Use(this WebApplication app, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        app.UseSwaggerDocumentation();
        app.UseLocalizer();


        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(
                Path.Combine(environment.ContentRootPath, PathsConst.Root)),
            RequestPath = $"/{PathsConst.Root}"
        });


        app.UseMiddleware<CulturesMiddleware>();
        app.UseMiddleware<ExceptionMiddleware>();
        if (configuration.GetValue<bool>("PlatformsMinimalVersions:Enable"))
            app.UseMiddleware<PlatFromMinimalsVersionsMiddleware>();


        // app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        foreach (var feature in app.Services.GetRequiredService<IEnumerable<IFeature>>())
            feature.UseService(app);

        app.MapControllers();
        return app;
    }
}