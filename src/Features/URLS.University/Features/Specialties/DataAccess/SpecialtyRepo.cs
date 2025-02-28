using Microsoft.EntityFrameworkCore;
using URLS.Data;
using URLS.Data.Entities;
using URLS.Shared.Exceptions;

namespace URLS.University.Features.Specialties.DataAccess;

public class SpecialtyRepo(UrlsContext urlsContext)
{
    public async Task<Specialty> GetSpecialtyAsync(int specId)
    {
        var specialty = await urlsContext.Specialties.SingleOrDefaultAsync(s => s.Id == specId);
        if (specialty == null)
            ThrowNotFound(specId);

        return specialty;
    }

    public async Task<Specialty> AddSpecialtyAsync(Specialty specialty)
    {
        ArgumentNullException.ThrowIfNull(specialty);

        urlsContext.Specialties.Add(specialty);
        await urlsContext.SaveAsync();
        return specialty;
    }

    public async Task<Specialty> UpdateSpecialtyAsync(Specialty specialty)
    {
        if (urlsContext.Entry(specialty).State is EntityState.Modified or EntityState.Unchanged)
        {
            await urlsContext.SaveAsync();
            return specialty;
        }

        throw new ArgumentException("Entity must be in modified state or unchanged state to be updated.");
    }

    public async Task DeleteSpecialtyAsync(int specId)
    {
        var spec = await GetSpecialtyAsync(specId);
        urlsContext.Specialties.Remove(spec);
        await urlsContext.SaveAsync();
    }

    private static void ThrowNotFound(int id)
        => throw new NotFoundException($"Specialty not found: Id = {id}");
}
