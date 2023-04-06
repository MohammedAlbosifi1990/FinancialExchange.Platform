
using FluentValidation.AspNetCore;

namespace Web.Api.Installers;

public static class FluentValidationsInstaller
{
    public static IServiceCollection AddValidations(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddFluentValidationAutoValidation(config => { config.DisableDataAnnotationsValidation = true; });
        return services;
    }
}