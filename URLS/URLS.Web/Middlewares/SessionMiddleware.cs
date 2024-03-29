﻿using Microsoft.AspNetCore.Authentication;
using URLS.Application.Services.Interfaces;
using URLS.Constants.APIResponse;
using URLS.Web.Extensions;

namespace URLS.Web.Middlewares
{
    public class SessionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ISessionManager _sessionManager;

        public SessionMiddleware(RequestDelegate next, ISessionManager sessionManager)
        {
            _next = next;
            _sessionManager = sessionManager;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var token = await httpContext.GetTokenAsync("access_token");
            if (httpContext.IsAuthenticationRequired())
            {
                if (token == null || !_sessionManager.IsActiveSession(token))
                {
                    httpContext.Response.StatusCode = 401;
                    await httpContext.Response.WriteAsJsonAsync(APIResponse.UnauthorizedResposne());
                    return;
                }
            }
            await _next(httpContext);
        }
    }

    public static class SessionMiddlewareExtensions
    {
        public static void UseSessionHandler(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<SessionMiddleware>();
        }
    }
}