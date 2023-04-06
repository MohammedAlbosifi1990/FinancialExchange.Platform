using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Core;
using Swashbuckle.AspNetCore.Annotations;

namespace Features.Cities;

public class ServiceInstaller : IFeature
{
    public  IServiceCollection AddService( IServiceCollection services, IConfiguration configuration)
    {

        return services;
    }

    public WebApplication UseService(WebApplication app)
    {
        var mediator = app.Services.GetRequiredService<IMediator>();

        app.MapGet("/Api/public/AuthFeature", () => Results.Ok())
            .WithMetadata(new SwaggerOperationAttribute(summary: "Summary", description: "Description Test"))
            .WithTags("ToDo");
        // app.MapGet("/test", 
        //     [SwaggerOperation(summary: "Summary1", description: "Descritption Test1")] () =>
        // {
        //     // Implementation
        // });
        return app;
    }
}