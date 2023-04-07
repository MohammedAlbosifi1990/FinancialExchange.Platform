using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Shared.Core.Domain.Constants;

namespace Web.Api.Installers;

public static class SwaggerInstaller
{
    public static IServiceCollection AddSwagger(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer()
            .AddSwaggerGen(setupAction: options =>
            {
                options.EnableAnnotations();
                options.SwaggerDoc(name: "Other", info: new OpenApiInfo
                {
                    Title = "Other Api",
                    Version = "Other.Api.V1",
                    Description = "This API features all Other available endpoints showing different API features."
                });

                options.SwaggerDoc(name: "Public", info: new OpenApiInfo
                {
                    Title = "Public Api",
                    Version = "Public.Api.V1",
                    Description = "This API features all public available endpoints showing different API features."
                });

                options.SwaggerDoc(name: "Protected", info: new OpenApiInfo
                {
                    Title = "Protected Api",
                    Version = "Protected.Api.V1",
                    Description = "This API features all Protected available endpoints showing different API features.",
                    Contact = new OpenApiContact()
                    {
                        Extensions = new Dictionary<string, IOpenApiExtension>()
                        {
                            {
                                "DateTime", new OpenApiDate(value: DateTime.UtcNow)
                            },
                            {
                                "Double", new OpenApiDouble(value: 20)
                            }
                        },
                    },
                });
                options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please Enter Only a Valid Access Token - ( Without Bearer Prefix )",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(securityRequirement: new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                        },
                        new string[] { }
                    }
                });
                options.DocInclusionPredicate(predicate: (name, api) =>
                {
                    if (name.ToLower().Contains(value: "Public".ToLower()))
                        return api.RelativePath != null && api.RelativePath.ToLower()
                            .StartsWith(value: RoutesConst.PublicPrefix.ToLower());
                    if (name.ToLower().Contains(value: "Protected".ToLower()))
                        return api.RelativePath != null && api.RelativePath.ToLower()
                            .StartsWith(value: RoutesConst.ProtectedPrefix.ToLower());

                    return api.RelativePath != null
                           && !api.RelativePath.ToLower().StartsWith(RoutesConst.ProtectedPrefix.ToLower())
                           && !api.RelativePath.ToLower().StartsWith(RoutesConst.PublicPrefix.ToLower());
                });
            });

        return services;
    }

    public static WebApplication UseSwaggerDocumentation(this WebApplication app)
    {
#if NET7_0_OR_GREATER && DEBUG
        if (!app.Environment.IsDevelopment()) return app;

        app.UseSwagger();
        app.UseSwaggerUI(setupAction: c =>
        {
            c.SwaggerEndpoint(url: "/swagger/Public/swagger.json", name: "Public API ");
            c.SwaggerEndpoint(url: "/swagger/Protected/swagger.json", name: "Protected API");
            c.SwaggerEndpoint(url: "/swagger/Other/swagger.json", name: "Other API");
        });
#endif


        return app;
    }
}