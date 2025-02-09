using Riok.Mapperly.Abstractions;
using URLS.Data.Entities;
using URLS.University.Features.Faculties.Dtos;
using URLS.University.Features.Universities.Dtos;
using Unik = URLS.Data.Entities.University;

namespace URLS.University;

[Mapper]
public static partial class UniversityMapper
{
    public static partial Unik MapToEntity(this CreateUniversityRequest university);
    public static partial UniversityDto MapToDto(this Unik university);
    public static partial void Populate(this CreateUniversityRequest universityDto, Unik university);
    public static partial IQueryable<UniversityDto> ProjectToDto(this IQueryable<Unik> universities);

    public static partial Faculty MapToEntity(this CreateFacultyRequest request);
    public static partial FacultyDto MapToDto(this Faculty faculty);
    public static partial void Populate(this CreateFacultyRequest request, Faculty faculty);
    public static partial IQueryable<FacultyDto> ProjectToDto(this IQueryable<Faculty> faculties);
}
