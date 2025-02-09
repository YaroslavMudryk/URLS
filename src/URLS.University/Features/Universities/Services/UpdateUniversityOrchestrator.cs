using FluentValidation;
using URLS.Shared.Auth;
using URLS.University.Features.Universities.DataAccess;
using URLS.University.Features.Universities.Dtos;

namespace URLS.University.Features.Universities.Services;

public class UpdateUniversityOrchestrator(
    IUserContext userContext,
    IValidator<CreateUniversityRequest> validator,
    UniversityRepo repo)
{
    public async Task<UniversityDto> UpdateUniversityAsync(int id, CreateUniversityRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        userContext.AssumeAuthenticated<BasicAuthenticatedUser>()
            .EnsureUserHasPermissions(UrlsClaims.Types.University, UrlsClaims.Values.Update);

        await validator.ValidateAsync(request);

        var univesityToUpdate = await repo.GetUniversityAsync(id);
        request.Populate(univesityToUpdate);
        var updatedUnivesity = await repo.UpdateUniversityAsync(univesityToUpdate);

        return updatedUnivesity.MapToDto();
    }
}
