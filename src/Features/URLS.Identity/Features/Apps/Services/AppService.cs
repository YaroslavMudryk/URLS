using Microsoft.EntityFrameworkCore;
using URLS.Data;
using URLS.Shared.Auth;
using URLS.Shared.Exceptions;

namespace URLS.Identity.Features.Apps.Services;

public class AppService(UrlsContext urlsContext)
{
    public async Task DeleteAppAsync(int appId, BasicAuthenticatedUser user)
    {
        var appToDelete = await urlsContext.Apps.Where(s => s.Id == appId).FirstOrDefaultAsync() ?? throw new NotFoundException($"App by id:{appId} not found");

        PermissionService.EnsureAppAvailable(appToDelete, user);

        urlsContext.Apps.Remove(appToDelete);
        await urlsContext.SaveAsync();
    }
}
