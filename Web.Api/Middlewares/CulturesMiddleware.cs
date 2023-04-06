
using System.Globalization;

namespace Web.Api.Middlewares;

public class CulturesMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public CulturesMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var headerCulture = context.Request.Headers["accept-lang"];
        var culture = _configuration["SystemCulture"] ?? "ar";
        if (!string.IsNullOrEmpty(headerCulture))
            culture = headerCulture;

        var cultureInfo = new CultureInfo(culture ?? "ar");

        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        Thread.CurrentThread.CurrentCulture = cultureInfo;
        Thread.CurrentThread.CurrentUICulture = cultureInfo;

        // context.Items["name"] = "mohammed";

        await _next(context);
    }
}