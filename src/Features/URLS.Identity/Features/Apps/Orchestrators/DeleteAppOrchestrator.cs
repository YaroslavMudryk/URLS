using URLS.Identity.Features.Apps.Services;
using URLS.Shared.Auth;

namespace URLS.Identity.Features.Apps.Orchestrators;

public class DeleteAppOrchestrator(
    IUserContext userContext,
    AppService appService)
{
    public async Task DeleteAppAsync(int appId)
    {
        var currentUser = userContext.AssumeAuthenticated<BasicAuthenticatedUser>();
        currentUser.EnsureUserHasPermissions(UrlsClaims.Types.App, UrlsClaims.Values.Delete);

        await appService.DeleteAppAsync(appId, currentUser);
    }
}
