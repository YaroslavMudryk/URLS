using FluentValidation;
using URLS.Data.Entities;
using URLS.Shared;
using URLS.Shared.Auth;
using URLS.University.Features.Specialties.DataAccess;
using URLS.University.Features.Specialties.Dtos;

namespace URLS.University.Features.Specialties.Services;

public class CreateSpecialtyOrchestrator(
    IUserContext userContext,
    IValidator<CreateSpecialtyRequest> validator,
    SpecialtyRepo repo)
{
    public async Task<SpecialtyDto> CreateSpecialtyAsync(CreateSpecialtyRequest request)
    {
        userContext.AssumeAuthenticated<BasicAuthenticatedUser>()
            .EnsureUserHasPermissions(UrlsClaims.Types.Specialty, UrlsClaims.Values.Create);

        await validator.ValidateAndThrowAsync(request);

        var specialtyToCreate = BuildEntity(request);
        var createdSpecialty = await repo.AddSpecialtyAsync(specialtyToCreate);

        return createdSpecialty.MapToDto();
    }

    private static Specialty BuildEntity(CreateSpecialtyRequest request)
    {
        var specialty = request.MapToEntity();
        specialty.Invite = Generator.CreateGroupInviteCode();
        return specialty;
    }
}
