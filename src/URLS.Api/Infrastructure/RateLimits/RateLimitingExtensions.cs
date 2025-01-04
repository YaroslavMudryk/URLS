using System.IdentityModel.Tokens.Jwt;
using System.Threading.RateLimiting;

namespace URLS.Api.Infrastructure.RateLimits;

public static class RateLimitingExtensions
{
    public static IServiceCollection AddUserRateLimiting(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.AddPolicy("UserLimiter", context =>
            {
                var userId = GetUserIdFromToken(context.Request.Headers.Authorization);

                if (userId == null)
                    return RateLimitPartition.GetNoLimiter(userId);

                return RateLimitPartition.GetFixedWindowLimiter(userId, partition =>
                    new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 100,
                        Window = TimeSpan.FromMinutes(1),
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 5,
                        AutoReplenishment = true
                    });
            });

            options.OnRejected = async (context, cancellationToken) =>
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.HttpContext.Response.WriteAsJsonAsync(new { Error = "Rate limit exceeded. Please try again later." }, cancellationToken);
            };
        });

        return services;
    }

    private static int? GetUserIdFromToken(string authorizationHeader)
    {
        if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            return null;

        var token = authorizationHeader["Bearer ".Length..];
        var jwtToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
        return Convert.ToInt32(jwtToken?.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value);
    }
}
