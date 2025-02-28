using Microsoft.EntityFrameworkCore;
using URLS.Data;
using URLS.Data.Entities;
using URLS.University.Features.Faculties.Dtos;
using Unik = URLS.Data.Entities.University;

namespace URLS.University.Features.Faculties.DataAccess;

public interface IFacultiesQuery
{
    Task<IReadOnlyList<FacultyDto>> GetFacultiesAsync(int universityId);
    Task<Unik> GetUniversityNullableAsync(int universityId);
    Task<Faculty> GetFacultyByOrderNullableAsync(int universityId, int order);
}

public class FacultiesQuery(UrlsContext urlsContext) : IFacultiesQuery
{
    public async Task<IReadOnlyList<FacultyDto>> GetFacultiesAsync(int universityId) =>
        await urlsContext.Faculties.Where(s => s.UniversityId == universityId).OrderBy(s => s.Order).ProjectToDto().ToListAsync();

    public async Task<Faculty> GetFacultyByOrderNullableAsync(int universityId, int order) =>
        await urlsContext.Faculties.AsNoTracking().Where(s => s.UniversityId == universityId && s.Order == order).FirstOrDefaultAsync();

    public async Task<Unik> GetUniversityNullableAsync(int universityId) =>
        await urlsContext.Universities.AsNoTracking().SingleOrDefaultAsync(s => s.Id == universityId);
}
