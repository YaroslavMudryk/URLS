using URLS.Data.Entities;
using URLS.Shared.Auth;
using URLS.Shared.Exceptions;

namespace URLS.Identity.Features.Apps.Services;

public class PermissionService
{
    public static void EnsureAppAvailable(App app, BasicAuthenticatedUser currentUser)
    {
        if (app.UserId == currentUser.UserId || currentUser.UserIsInRoles([UrlsConstants.Roles.Admin]))
            return;

        throw new UnauthorizedException();
    }
}
