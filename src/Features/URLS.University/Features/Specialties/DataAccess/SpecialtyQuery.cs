using Microsoft.EntityFrameworkCore;
using URLS.Data;
using URLS.Data.Entities;
using URLS.University.Features.Specialties.Dtos;

namespace URLS.University.Features.Specialties.DataAccess;

public interface ISpecialtiesQuery
{
    Task<List<SpecialtyDto>> GetSpecialtiesAsync(int facultyId);
    Task<Faculty> GetFacultyNullabeAsync(int facultyId);
}

public class SpecialtiesQuery(UrlsContext urlsContext) : ISpecialtiesQuery
{
    public async Task<Faculty> GetFacultyNullabeAsync(int facultyId) =>
        await urlsContext.Faculties.AsNoTracking().SingleOrDefaultAsync(s => s.Id == facultyId);

    public async Task<List<SpecialtyDto>> GetSpecialtiesAsync(int facultyId) =>
        await urlsContext.Specialties.AsNoTracking().Where(s => s.FacultyId == facultyId).OrderBy(s => s.Name).ProjectToDto().ToListAsync();
}
