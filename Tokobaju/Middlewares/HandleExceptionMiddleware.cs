using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Tokobaju.Dto;
using Tokobaju.Exceptions;

namespace Tokobaju.Middlewares;

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
        catch (BadRequestException e)
        {
            await HandleExceptionAsync(context, e);
        }
        catch (NotFoundException e)
        {
            await HandleExceptionAsync(context, e);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        switch (exception)
        {
            case BadRequestException:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            case NotFoundException:
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                break;
            case UnauthorizedException:
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                break;
            case not null:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        var error = new ErrorDto
        {
            StatusCode = context.Response.StatusCode,
            Message = exception != null ? exception.Message : "error"
        };

        if (exception!.InnerException != null)
        {
            error.Message = exception.InnerException.Message;
        }

        _logger.LogError(error.Message);
        await context.Response.WriteAsJsonAsync(error);
    }
}