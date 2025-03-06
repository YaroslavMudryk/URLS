using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using URLS.Data;
using URLS.Data.Entities;
using URLS.Shared.Exceptions;

namespace URLS.Identity.Features.Apps.DataAccess;

public class AppRepo(UrlsContext urlsContext)
{
    public async Task<IDbContextTransaction> BeginTransactionAsync()
        => await urlsContext.Database.BeginTransactionAsync();

    public async Task<App> GetAppAsync(int id)
    {
        var app = await urlsContext.Apps.FirstOrDefaultAsync(x => x.Id == id);
        if (app is null)
            ThrowNotFound(id);

        return app;
    }

    public async Task<App> AddAppAsync(App app)
    {
        ArgumentNullException.ThrowIfNull(app);

        urlsContext.Apps.Add(app);
        await urlsContext.SaveAsync();

        return app;
    }

    public async Task<App> UpdateAppAsyc(App app)
    {
        ArgumentNullException.ThrowIfNull(app);

        if (urlsContext.Entry(app).State is EntityState.Modified or EntityState.Unchanged)
        {
            await urlsContext.SaveAsync();
            return app;
        }

        throw new ArgumentException("Entity must be in modified state or unchanged state to be updated.");
    }

    public async Task DeleteAppAsync(int id)
    {
        var app = await urlsContext.Apps.FirstOrDefaultAsync(x => x.Id == id);
        if (app is null)
            ThrowNotFound(id);

        urlsContext.Apps.Remove(app!);
        await urlsContext.SaveAsync();
    }
    private static void ThrowNotFound(int id)
        => throw new NotFoundException($"App not found: Id = {id}.");
}
