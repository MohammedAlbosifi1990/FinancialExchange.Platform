using System.Net;
using FluentValidation;
using Newtonsoft.Json;
using Shared.Core.Domain.Exceptions;
using Shared.Core.Domain.Extensions;
using Shared.Core.Domain.Models;

namespace Web.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";
            string? response;
            switch (ex)
            {
                case BaseException exception:
                    context.Response.StatusCode = exception.StatusCode;
                    response = JsonConvert.SerializeObject(exception.ToResponse());
                    break;
                case ValidationException validationException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    var validationFailure = validationException.Errors.FirstOrDefault();
                    var message = "الرجاء التأكد من البيانات المرسلة";
                    if (validationFailure != null)
                        message = validationFailure.ErrorMessage;
                    response = JsonConvert.SerializeObject(ApiResponse.BadRequest(message));
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response = JsonConvert.SerializeObject(ApiResponse.BadRequest( " حدث خطأ أثناء معالجة الطلب"));
                    break;
            }
            
            await context.Response.WriteAsync(response);
        }
    }
}
