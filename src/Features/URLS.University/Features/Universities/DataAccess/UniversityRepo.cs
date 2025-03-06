using Microsoft.EntityFrameworkCore;
using URLS.Data;
using URLS.Shared.Exceptions;
using Unik = URLS.Data.Entities.University;

namespace URLS.University.Features.Universities.DataAccess;

public class UniversityRepo(UrlsContext urlsContext)
{
    public async Task<Unik> AddUniversityAsync(Unik university)
    {
        ArgumentNullException.ThrowIfNull(university);

        urlsContext.Universities.Add(university);
        await urlsContext.SaveAsync();
        return university;
    }

    public async Task<Unik> GetUniversityAsync(int id)
    {
        var university = await urlsContext.Universities.FirstOrDefaultAsync(x => x.Id == id);
        if (university is null)
            ThrowNotFound(id);

        return university;
    }

    public async Task<Unik> UpdateUniversityAsync(Unik university)
    {
        if (urlsContext.Entry(university).State is EntityState.Modified or EntityState.Unchanged)
        {
            await urlsContext.SaveAsync();
            return university;
        }

        throw new ArgumentException("Entity must be in modified state or unchanged state to be updated.");
    }

    private static void ThrowNotFound(int id)
        => throw new NotFoundException($"University not found: Id = {id}");
}
