using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Core;

namespace Features.Companies;

public class ServiceInstaller : IFeature
{
    public  IServiceCollection AddService( IServiceCollection services, IConfiguration configuration)
    {

        return services;
    }

    public WebApplication UseService(WebApplication app)
    {

        return app;
    }
}