﻿using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Shared.Core.Domain.Models;
using Shared.Core.Domain.Models.Options;

namespace Web.Api.Installers;

public static class ControllersInstaller
{
    public static IServiceCollection AddControllers(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<List<PlatformMinimalVersionModel>>(configuration.GetSection("PlatformsMinimalVersions"));
        services.AddControllers()
            .AddOData(options =>
                options.Select().Filter().Count().OrderBy().Expand())
            .AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var modelState = actionContext.ModelState.Values;
                    var state = modelState.FirstOrDefault();
                    var error = state!.Errors[0].ErrorMessage;

                    return new BadRequestObjectResult(ApiResponse.BadRequest(error));
                };
            });

        return services;
    }
}