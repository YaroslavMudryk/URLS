using URLS.Identity.Features.Apps.DataAccess;
using URLS.Identity.Features.Apps.Dtos;
using URLS.Shared.Auth;

namespace URLS.Identity.Features.Apps.Orchestrators;

public class GetAppsOrchestrator(
    IUserContext userContext,
    AppsQuery query)
{
    public async Task<AppsResponse> GetAppsAsync()
    {
        var currentUser = userContext.AssumeAuthenticated<BasicAuthenticatedUser>();
        currentUser.EnsureUserHasPermissions(UrlsClaims.Types.App, UrlsClaims.Values.ViewAll);

        var isAdmin = currentUser.UserIsInRoles([UrlsConstants.Roles.Admin]);

        return new AppsResponse
        {
            Apps = await query.GetAppsAsync(currentUser.UserId, isAdmin, 1, 100),
            Meta = await query.GetAppsMetaAsync(1, isAdmin, 100)
        };
    }
}
