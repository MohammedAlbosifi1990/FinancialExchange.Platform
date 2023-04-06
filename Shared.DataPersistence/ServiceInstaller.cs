using Features.Authentications.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shared.Core.Domain.Entities;
using Shared.Core.Domain.Models.Options;
using Shared.Core.Repositories;
using Shared.DataPersistence.Data.Db;
using Shared.DataPersistence.Repositories;

namespace Shared.DataPersistence;

public static class ServiceInstaller
{
    public static IServiceCollection AddDataPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("db")
                , builder => { builder.EnableRetryOnFailure(2, TimeSpan.FromSeconds(5), null); }));

        services.AddScoped<ICitiesRepository, CitiesRepository>();
        
        
        var optionValue = services.BuildServiceProvider().GetRequiredService<IOptions<AuthenticationsOption>>().Value;
        services.AddIdentity<User, ApplicationRole>(
                options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Lockout.AllowedForNewUsers = true;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(optionValue.LockoutTimeInMinute);
                    options.Lockout.MaxFailedAccessAttempts = optionValue.LockoutCount;
                    options.SignIn.RequireConfirmedEmail = false;
                    options.User.RequireUniqueEmail = true;
                }
            )
            .AddErrorDescriber<MultiLanguageIdentityErrorDescriber>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = $"/Identity/Account/Login";
            options.LogoutPath = $"/Identity/Account/Logout";
            options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
        });
        return services;
    }
}