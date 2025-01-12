using URLS.Identity.Features.Apps.DataAccess;
using URLS.Identity.Features.Apps.Dtos;
using URLS.Identity.Features.Apps.Services;
using URLS.Shared.Auth;

namespace URLS.Identity.Features.Apps.Orchestrators;

public class GetAppOrchestrator(
    IUserContext userContext,
    AppsQuery query,
    AppsEnricherService enricherService)
{
    public async Task<AppDto> GetAppAsync(int appId)
    {
        var currentUser = userContext.AssumeAuthenticated<BasicAuthenticatedUser>();
        currentUser.EnsureUserHasPermissions(UrlsClaims.Types.App, UrlsClaims.Values.View);

        var app = await query.GetAppByIdAsync(appId);

        PermissionService.EnsureAppAvailable(app, currentUser);

        return await enricherService.GetEnrichedAppAsync(app);
    }
}
