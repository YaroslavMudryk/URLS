using System.Diagnostics;
using URLS.Api.Logging;

namespace URLS.Api.Infrastructure.Middlewares;

public class LoggingMiddleware(ILogger<LoggingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(next);

        LogRequest(context.Request);
        var stopwatch = Stopwatch.StartNew();
        await next(context);
        LogResponse(context.Request, context.Response, stopwatch.ElapsedMilliseconds);
    }

    private void LogRequest(HttpRequest request)
    {
        var logRequest = RequestInfo.Create(request);
        logger.LogRequestReceived(logRequest);
    }

    private void LogResponse(HttpRequest request, HttpResponse response, long elapsedMilliseconds)
    {
        var logResponse = ResponseInfo.Create(request, response, elapsedMilliseconds);
        logger.LogResponseSent(logResponse);
    }
}

public static partial class LoggingMiddlewareLogger
{
    [LoggerMessage(LogLevel.Information, "RequestReceived", EventName = "RequestReceived")]
    public static partial void LogRequestReceived(this ILogger logger,
        [LogProperties(OmitReferenceName = true)] RequestInfo requestInfo);

    [LoggerMessage(LogLevel.Information, "ResponseSent", EventName = "ResponseSent")]
    public static partial void LogResponseSent(this ILogger logger,
        [LogProperties(OmitReferenceName = true)] ResponseInfo responseInfo);
}