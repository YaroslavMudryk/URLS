using Microsoft.EntityFrameworkCore;
using URLS.Data;
using URLS.Data.Entities;
using URLS.Identity.Features.Apps.Dtos;
using URLS.Shared;
using URLS.Shared.Exceptions;

namespace URLS.Identity.Features.Apps.DataAccess;

public interface IAppsQuery
{
    Task<App> GetAppByIdAsync(int appId);
    Task<IReadOnlyList<AppShortDto>> GetAppsAsync(int userId, bool isCurrentUserAdmin, int page, int per);
    Task<Meta> GetAppsMetaAsync(int userId, bool isCurrentUserAdmin, int per);
    Task<bool> IsAppNameUniquenessAsync(string appName);
    Task<bool> IsAppNameUniquenessAsync(string appName, int id);
    Task<IReadOnlyList<int>> GetDiffClaimIdsAsync(IReadOnlyList<int> claimIds);
}

public class AppsQuery(UrlsContext urlsContext) : IAppsQuery
{
    public async Task<App> GetAppByIdAsync(int appId) =>
        await urlsContext.Apps.AsNoTracking().Where(s => s.Id == appId).FirstOrDefaultAsync() ?? throw new NotFoundException($"App by id:{appId} not found");

    public async Task<IReadOnlyList<AppShortDto>> GetAppsAsync(int userId, bool isCurrentUserAdmin, int page, int per)
    {
        return await GetAppsWithoutPaginationQuery(userId, isCurrentUserAdmin)
            .OrderByDescending(s => s.CreatedAt)
            .Skip(PaginationHelper.GetSkip(page, per)).Take(per)
            .ProjectToDto()
            .ToListAsync();
    }

    public async Task<Meta> GetAppsMetaAsync(int userId, bool isCurrentUserAdmin, int per)
    {
        var totalCount = await GetAppsWithoutPaginationQuery(userId, isCurrentUserAdmin).CountAsync();
        return Meta.GetMeta(totalCount, per);
    }

    public async Task<IReadOnlyList<int>> GetDiffClaimIdsAsync(IReadOnlyList<int> claimIds)
    {
        var retrievedClaimIds = await urlsContext
            .Claims
            .AsNoTracking()
            .Where(s => claimIds.Contains(s.Id))
            .Select(s => s.Id)
            .ToListAsync();

        return retrievedClaimIds.Except(claimIds).ToList();
    }

    public async Task<bool> IsAppNameUniquenessAsync(string appName) =>
        !(await urlsContext.Apps.AsNoTracking().AnyAsync(s => s.Name == appName));

    public async Task<bool> IsAppNameUniquenessAsync(string appName, int id) =>
        !(await urlsContext.Apps.AsNoTracking().AnyAsync(s => s.Name == appName && s.Id != id));

    private IQueryable<App> GetAppsWithoutPaginationQuery(int userId, bool isCurrentUserAdmin)
    {
        var query = urlsContext.Apps.AsQueryable();

        if (!isCurrentUserAdmin)
            query = query.Where(s => s.UserId == userId);

        return query;
    }
}
