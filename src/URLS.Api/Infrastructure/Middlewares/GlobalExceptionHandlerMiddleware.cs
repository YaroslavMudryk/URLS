using URLS.Shared.Exceptions;
using URLS.Shared;
using FluentValidation;
using URLS.Shared.Api;

namespace URLS.Api.Infrastructure.Middlewares;

public class GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(next);

        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var validationErrors = ex.Errors
                    .ToLookup(x => x.PropertyName, x => x.ErrorMessage)
                    .ToDictionary(x => x.Key, x => x.ToArray());

            await context.Response.WriteAsJsonAsync(ApiResponse.ValidationFail(validationErrors), Settings.Json);
        }
        catch (HttpResponseException ex)
        {
            logger.LogControlledException(ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ex.StatusCode;
            await context.Response.WriteAsJsonAsync(ApiResponse.Fail(ex.Message), Settings.Json);
        }
        catch (BadHttpRequestException ex)
        {
            logger.LogBadRequestException(ex.Message);
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(ApiResponse.Fail("Bad request"), Settings.Json);
        }
        catch (NotImplementedException nie)
        {
            logger.LogUnexpectedException(nie);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(ApiResponse.Fail("This functionality is planned but not yet implemented, it will be available in the near future"), Settings.Json);
        }
        catch (Exception ex)
        {
            logger.LogUnexpectedException(ex);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(ApiResponse.Fail("Server error"), Settings.Json);
        }
    }
}

public static partial class GlobalExceptionHandlerMiddlewareLogger
{
    [LoggerMessage(LogLevel.Warning, "Controlled exception", EventName = "GlobalExceptionHandler.ControlledException")]
    public static partial void LogControlledException(this ILogger logger, string errorMessage);

    [LoggerMessage(LogLevel.Warning, "Bad request exception", EventName = "GlobalExceptionHandler.BadRequest")]
    public static partial void LogBadRequestException(this ILogger logger, string errorMessage);

    [LoggerMessage(LogLevel.Error, "Unexpected exception", EventName = "GlobalExceptionHandler.UnexpectedException")]
    public static partial void LogUnexpectedException(this ILogger logger, Exception ex);
}
