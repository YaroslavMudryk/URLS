using Microsoft.EntityFrameworkCore;
using URLS.Data;
using URLS.Data.Entities;

namespace URLS.Identity.Features.Apps.Services;

public class AppClaimsService(
    UrlsContext urlsContext,
    TimeProvider timeProvider)
{
    public async Task UpdateClaimsForAppAsync(int appId, IReadOnlyList<int> newClaimIds)
    {
        var appClaimsToDelete = await urlsContext.AppClaims.Where(s => s.AppId == appId).ToListAsync();
        if (appClaimsToDelete.Any())
        {
            urlsContext.AppClaims.RemoveRange(appClaimsToDelete);
            await urlsContext.SaveAsync();
        }

        var utcNow = timeProvider.GetUtcNow().UtcDateTime;
        var newAppClaims = newClaimIds.Select(s => new AppClaim
        {
            AppId = appId,
            ClaimId = s,
            CreatedAt = utcNow,
            UpdatedAt = utcNow
        });
        urlsContext.AppClaims.AddRange(newAppClaims);
        await urlsContext.SaveAsync();
    }
}