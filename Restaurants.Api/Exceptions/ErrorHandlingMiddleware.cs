
using Azure;
using Microsoft.AspNetCore.Http;
using Resturants.Domain.Exceptions;

namespace Restaurants.Api.Exceptions;
public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (NotFoundException ex)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync($"{ex.Message}");
            logger.LogWarning(ex, ex.Message);
        }
        catch (ForbidException)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync($"Access Forbidden");
        }
        catch (UnAuthorizedException ex)
        {
            logger.LogError(ex, "UnAuthorizedException caught in exception handler middleware: {ExceptionMessage}", ex.Message);
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Something Went Wrong");
        }
    }
}
