using FluentValidation;
using URLS.Shared.Auth;
using URLS.University.Features.Specialties.DataAccess;
using URLS.University.Features.Specialties.Dtos;

namespace URLS.University.Features.Specialties.Services;

public class UpdateSpecialtyOrchestrator(
    IUserContext userContext,
    IValidator<CreateSpecialtyRequest> validator,
    SpecialtyRepo repo)
{
    public async Task<SpecialtyDto> UpdatedSpecialtyAsync(int specId, CreateSpecialtyRequest request)
    {
        userContext.AssumeAuthenticated<BasicAuthenticatedUser>()
            .EnsureUserHasPermissions(UrlsClaims.Types.Specialty, UrlsClaims.Values.Update);

        await validator.ValidateAndThrowAsync(request);

        var specialtyToUpdate = await repo.GetSpecialtyAsync(specId);
        request.Populate(specialtyToUpdate);
        var updatedSpecialty = await repo.UpdateSpecialtyAsync(specialtyToUpdate);

        return updatedSpecialty.MapToDto();
    }
}
