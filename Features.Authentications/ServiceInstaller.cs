using System.Text;
using Features.Authentications.Services.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Shared.Core;
using Shared.Core.Domain.Models.Options;

namespace Features.Authentications;

public class ServiceInstaller : IFeature
{
    public IServiceCollection AddService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.Configure<AuthenticationsOption>(configuration.GetSection("AuthRequired"));

        
        // services.AddTransient<IAuthorizationHandler, AllowUsersHandler>();
        // services.AddAuthorization(opts => {
        //     opts.AddPolicy("AllowTom", policy => {
        //         policy.AddRequirements(new AllowUserPolicy("tom"));
        //     });
        // });
        // services.AddSingleton<IAuthorizationHandler, UserLevelRequirementHandler>();


        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;

                options.Events = new JwtBearerEvents()
                {
                    OnTokenValidated = _ => Task.CompletedTask,
                    OnMessageReceived = _ => Task.CompletedTask
                };
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = configuration["AuthRequired:JWTOptions:ValidAudience"],
                    ValidIssuer = configuration["AuthRequired:JWTOptions:ValidIssuer"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthRequired:JWTOptions:Secret"]!)),
                    ClockSkew = TimeSpan.Zero,
                };
            });
        return services;
    }

    public WebApplication UseService(WebApplication app)
    {
        return app;
    }
}