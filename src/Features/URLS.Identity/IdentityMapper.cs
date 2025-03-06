using Riok.Mapperly.Abstractions;
using URLS.Data.Entities;
using URLS.Identity.Features.Apps.Dtos;

namespace URLS.Identity;

[Mapper]
public static partial class IdentityMapper
{
    public static partial IQueryable<AppShortDto> ProjectToDto(this IQueryable<App> apps);
    public static partial AppDto MapToDto(this App app);
    public static partial App MapToEntity(this CreateAppRequest request);
    [MapperIgnoreTarget(nameof(app.Id))]
    public static partial void Populate(this UpdateAppRequest request, App app);
    public static partial CreatedAppResponse MapToCreatedResponse(this App app);
    public static partial UpdatedAppResponse MapToUpdatedResponse(this App app);
}
