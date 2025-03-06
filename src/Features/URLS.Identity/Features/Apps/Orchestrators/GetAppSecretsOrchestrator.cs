using URLS.Identity.Features.Apps.DataAccess;
using URLS.Identity.Features.Apps.Dtos;
using URLS.Identity.Features.Apps.Services;
using URLS.Shared.Auth;

namespace URLS.Identity.Features.Apps.Orchestrators;

public class GetAppSecretsOrchestrator(
    IUserContext userContext,
    AppsQuery query)
{
    public async Task<AppSecretResponse> GetAppSecretsAsync(int appId)
    {
        var currentUser = userContext.AssumeAuthenticated<BasicAuthenticatedUser>();
        currentUser.EnsureUserHasPermissions(UrlsClaims.Types.App, UrlsClaims.Values.View);

        var app = await query.GetAppByIdAsync(appId);

        PermissionService.EnsureAppAvailable(app, currentUser);

        return new AppSecretResponse
        {
            AppId = app.Id,
            ClientId = app.ClientId,
            ClientSecret = app.ClientSecret,
        };
    }
}
