using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using URLS.Identity.Features.Apps.Dtos;
using URLS.Identity.Features.Apps.Orchestrators;
using URLS.Identity.Features.Claims.Dtos;
using URLS.Identity.Features.Claims.Orchestrators;
using URLS.Identity.Features.Roles.Dtos;
using URLS.Identity.Features.Roles.Orchestrators;
using URLS.Identity.Features.Users.Dtos;
using URLS.Identity.Features.Users.Orchestrators;

namespace URLS.Identity;

public static class IdentityEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/api/v1/users/current",
            async (GetUserOrchestrator orc) =>
            {
                var currentUser = await orc.GetCurrentUserAsync();
                return Results.Ok(currentUser);
            });
        
        app.MapPost("/api/v1/users/search",
            async (SearchUsersRequest request, SearchUsersOrchestrator orc) =>
            {
                var users = await orc.SearchUsersAsync(request);
                return Results.Ok(users);
            });

        app.MapGet("/api/v1/users/{userId:int}",
            async (int userId, GetUserByIdOrchestrator orc) =>
            {
                var user = await orc.GetUserAsync(userId);
                return Results.Ok(user);
            });

        
        app.MapPost("/api/v1/identity/sign-in",
            async (SignInRequest request, SignInOrchestrator orc) =>
            {
                var signInResponse = await orc.SignInAsync(request);
                return Results.Ok(signInResponse);
            });

        app.MapPost("/api/v1/identity/sign-up",
            async (SignUpRequest request, SignUpOrchestrator orc) =>
            {
                var signUpResponse = await orc.SignUpAsync(request);
                return Results.Ok(signUpResponse);
            });

        app.MapPost("/api/v1/identity/sign-out",
            async (SignOutRequest request, SignOutOrchestrator orc) =>
            {
                await orc.SignOutAsync(request);
                return Results.NoContent();
            });

        app.MapPost("/api/v1/identity/refresh-token",
            async (RefreshTokenRequest request, RefreshTokenOrchestrator orc) =>
            {
                var refreshTokenResponse = await orc.RefreshTokenAsync(request);
                return Results.Ok(refreshTokenResponse);
            });

        app.MapGet("/api/v1/identity/sessions",
            async (GetSessionsOrchestrator orc) =>
            {
                var activeSessions = await orc.GetUserSessionsAsync();
                return Results.Ok(activeSessions);
            });

        app.MapDelete("/api/v1/identity/sessions/{sessionId:guid}",
            async (Guid sessionId, DeleteSessionOrchestrator orc) =>
            {
                await orc.DeleteSessionAsync(sessionId);
                return Results.NoContent();
            });

        app.MapDelete("/api/v1/identity/sessions/other",
            async (DeleteSessionsOrchestrator orc) =>
            {
                await orc.DeleteOtherSessionsAsync();
                return Results.NoContent();
            });


        app.MapPost("/api/v1/identity/roles",
            async (CreateRoleRequest request, CreateRoleOrchestrator orc) =>
            {
                var createdRole = await orc.CreateRoleAsync(request);
                return Results.Ok(createdRole);
            });

        app.MapGet("/api/v1/identity/roles",
            async (GetRolesOrchestrator orc) =>
            {
                var allRoles = await orc.GetRolesAsync();
                return Results.Ok(allRoles);
            });

        app.MapGet("/api/v1/identity/roles/{roleId:int}",
            async (int roleId, GetRoleOrchestrator orc) =>
            {
                var role = await orc.GetRoleAsync(roleId);
                return Results.Ok(role);
            });

        app.MapPut("/api/v1/identity/roles/{roleId:int}",
            async (int roleId, UpdateRoleRequest request, UpdateRoleOrchestrator orc) =>
            {
                var updatedRole = await orc.UpdateRoleAsync(roleId, request);
                return Results.Ok(updatedRole);
            });

        app.MapDelete("/api/v1/identity/roles/{roleId:int}",
            async (int roleId, DeleteRoleOrchestrator orc) =>
            {
                await orc.DeleteRoleAsync(roleId);
                return Results.NoContent();
            });


        app.MapGet("/api/v1/identity/claims",
            async (GetClaimsOrchestrator orc) =>
            {
                var allClaims = await orc.GetClaimsAsync();
                return Results.Ok(allClaims);
            });

        app.MapPut("/api/v1/identity/claims/{claimId:int}",
            async (int claimId, UpdateClaimRequest request, UpdateClaimOrchestrator orc) =>
            {
                var updatedClaim = await orc.UpdateClaimAsync(claimId, request);
                return Results.Ok(updatedClaim);
            });


        app.MapPost("/api/v1/identity/apps",
            async (CreateAppRequest request, CreateAppOrchestrator orc) =>
            {
                var createdApp = await orc.CreateAppAsync(request);
                return Results.Ok(createdApp);
            });

        app.MapGet("/api/v1/identity/apps",
            async (GetAppsOrchestrator orc) =>
            {
                var allApps = await orc.GetAppsAsync();
                return Results.Ok(allApps);
            });

        app.MapGet("/api/v1/identity/apps/{appId:int}",
            async (int appId, GetAppOrchestrator orc) =>
            {
                var app = await orc.GetAppAsync(appId);
                return Results.Ok(app);
            });

        app.MapGet("/api/v1/identity/apps/{appId:int}/secrets",
            async (int appId, GetAppSecretsOrchestrator orc) =>
            {
                var appSecrets = await orc.GetAppSecretsAsync(appId);
                return Results.Ok(appSecrets);
            });

        app.MapPut("/api/v1/identity/apps/{appId:int}",
            async (int appId, UpdateAppRequest request, UpdateAppOrchestrator orc) =>
            {
                var updatedApp = await orc.UpdateAppAsync(appId, request);
                return Results.Ok(updatedApp);
            });

        app.MapDelete("/api/v1/identity/apps/{appId:int}",
            async (int appId, DeleteAppOrchestrator orc) =>
            {
                await orc.DeleteAppAsync(appId);
                return Results.NoContent();
            });
    }
}
