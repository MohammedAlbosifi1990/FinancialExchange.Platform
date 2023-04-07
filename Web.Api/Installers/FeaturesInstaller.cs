using FluentValidation;
using MediatR;
using Shared.Core;
using Shared.Core.Behavior;
using Shared.Core.Domain.Models.Options;

namespace Web.Api.Installers;

public static class FeaturesInstaller
{
    public static IServiceCollection AddFeatures(this IServiceCollection services,
        IConfiguration configuration)
    {
        var myArray = configuration.GetSection("ActiveFeatures").Get<List<FeatureOption>>();
        if (myArray == null || !myArray.Any())
            return services;

        services.Configure<List<FeatureOption>>(configuration.GetSection("ActiveFeatures"));

        var citiesFeature = myArray.FirstOrDefault(f => f.Name.ToLower().Equals("Cities".ToLower()));
        if (citiesFeature is { IsActive: true })
            services.AddFeatures<Features.Cities.ServiceInstaller>(configuration);

        var authenticationsFeature = myArray.FirstOrDefault(f => f.Name.ToLower().Equals("Authentications".ToLower()));
        if (authenticationsFeature is { IsActive: true })
            services.AddFeatures<Features.Authentications.ServiceInstaller>(configuration);


        return services;
    }

    private static void AddFeatures<TFeature>(this IServiceCollection services,
        IConfiguration configuration,
        bool isActive = true,
        bool userMediator = true)
        where TFeature : IFeature, new()
    {
        if (!isActive) return;

        var feature = new TFeature();
        feature.AddService(services, configuration);

        if (userMediator)
        {
            services.AddMediatR(typeof(TFeature));
            services.AddValidatorsFromAssembly(typeof(TFeature).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        }

        services.AddAutoMapper(typeof(TFeature));


        services.AddSingleton<IFeature>(feature);
    }
}