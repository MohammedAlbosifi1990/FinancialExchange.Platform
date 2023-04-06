
namespace Web.Api.Middlewares;

public class PlatFromMinimalsVersionsMiddleware
{
    private readonly RequestDelegate _next;
    // private readonly IEnumerable<PlatformMinimalVersionModel> _platformVersions;

    public PlatFromMinimalsVersionsMiddleware(RequestDelegate next)
    //     IOptions<List<PlatformMinimalVersionModel>> options)
    {
    _next = next;
    //     // _platformVersions = options.Value;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // try
        // {
            // var version = context.Request.Headers.SingleOrDefault(h => h.Key == "version").Value;
            // var platform = context.Request.Headers.SingleOrDefault(h => h.Key == "platform").Value;

            // if (string.IsNullOrEmpty(version))
            //     throw new PlatFormVersionException($"You Must Set Version Into Header");
            //
            // if ( string.IsNullOrEmpty(platform))
            //     platform = "mobile";
            //
            //
            // var optionPlatform = _platformVersions.SingleOrDefault(p => p.PlatForm == platform);
            //
            // if (optionPlatform!.Version >  int.Parse(version!))
            //     throw new PlatFormVersionException($"You Must Set Version Equal Or Grater Than {optionPlatform.Version}");
            await _next(context);
        // }
        // catch (Exception ex)
        // {
            // context.Response.ContentType = "application/json";
            // string response ;
            // if (ex is BaseException exception)
            // {
            //     context.Response.StatusCode = exception.StatusCode;

                // switch (exception)
                // {
                //     case PlatFormVersionException versionException:
                //         
                //         break;
                // }
                
                // response = JsonConvert.SerializeObject(ApiResponse
            //         .InternalServerError(exception.Message).ToString());
            // }
            // else
            // {
            //     context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //     response = "Internal Server Error from the custom middleware.";
            // }
            //
            // await context.Response.WriteAsync(response);
        // }
    }
}