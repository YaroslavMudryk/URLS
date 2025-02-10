using Microsoft.EntityFrameworkCore;
using URLS.Data;
using URLS.Data.Entities;
using URLS.Shared.Exceptions;

namespace URLS.University.Features.Faculties.DataAccess;

public class FacultiesRepo(UrlsContext urlsContext)
{
    public async Task<Faculty> AddFacultyAsync(Faculty faculty)
    {
        ArgumentNullException.ThrowIfNull(faculty);

        urlsContext.Faculties.Add(faculty);
        await urlsContext.SaveAsync();
        return faculty;
    }

    public async Task<Faculty> GetFacultyAsync(int id)
    {
        var faculty = await urlsContext.Faculties.FirstOrDefaultAsync(x => x.Id == id);
        if (faculty is null)
            ThrowNotFound(id);

        return faculty;
    }

    public async Task<Faculty> UpdateTagAsync(Faculty faculty)
    {
        if (urlsContext.Entry(faculty).State is EntityState.Modified or EntityState.Unchanged)
        {
            await urlsContext.SaveAsync();
            return faculty;
        }

        throw new ArgumentException("Entity must be in modified state or unchanged state to be updated.");
    }

    public async Task DeleteFacultyAsync(int id)
    {
        var faculty = await GetFacultyAsync(id);

        urlsContext.Remove(faculty!);
        await urlsContext.SaveAsync();
    }

    private static void ThrowNotFound(int id)
        => throw new NotFoundException($"Faculty not found: Id = {id}");
}
