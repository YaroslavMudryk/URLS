using Serilog.Context;
using URLS.Api.Infrastructure.Constants;
using URLS.Api.Infrastructure.Helpers;
using URLS.Shared.Auth;

namespace URLS.Api.Infrastructure.Middlewares;

public class CorrelationContextMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(next);

        var correlationId = context.GetOrAssignCorrelationId();
        ((UserContext)context.RequestServices.GetService<IUserContext>()).CorrelationId = correlationId;
        context.Response.OnStarting(() =>
        {
            context.Response.Headers.Append(HttpHeadersConstants.CorrelationId, correlationId);
            return Task.CompletedTask;
        });

        using (LogContext.PushProperty("CorrelationId", correlationId))
            await next(context);
    }
}
