using Riok.Mapperly.Abstractions;
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
}
