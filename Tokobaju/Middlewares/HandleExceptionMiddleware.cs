using System.Net;
using Tokobaju.Dto;

namespace Tokobagus.Middlewares;

public class HandleExceptionMiddleware : IMiddleware
{
    private readonly ILogger<HandleExceptionMiddleware> _logger;

    public HandleExceptionMiddleware(ILogger<HandleExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var error = new Error
        {
            StatusCode = context.Response.StatusCode,
            Message = exception.Message
        };

        _logger.LogError(exception.ToString());
        await context.Response.WriteAsJsonAsync(error);
    }
}