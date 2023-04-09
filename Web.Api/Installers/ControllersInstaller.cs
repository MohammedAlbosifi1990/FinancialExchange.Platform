using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shared.Core.Domain.Models;

namespace Web.Api.Installers;

public static class ControllersInstaller
{
    public static IServiceCollection AddControllers(this IServiceCollection services,
        IConfiguration configuration)
    {
        
        services.AddControllers()
            .AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var modelState = actionContext.ModelState.Values;
                    var state = modelState.FirstOrDefault(m=>m.ValidationState==ModelValidationState.Invalid);
                    string error;
                    if (state!=null)
                        error = state.Errors.Any() ? state!.Errors[0].ErrorMessage : "UnKnow Validation Error";
                    else
                        error = "UnKnow Validation Error";

                    return new BadRequestObjectResult(ApiResponse.BadRequest(error));
                };
            });

        return services;
    }
}