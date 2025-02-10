using URLS.Shared;
using URLS.Shared.Auth;
using URLS.University.Features.Specialties.DataAccess;
using URLS.University.Features.Specialties.Dtos;

namespace URLS.University.Features.Specialties.Services;

public class ChangeSpecialtySecretOrchestrator(
    IUserContext userContext,
    SpecialtyRepo repo)
{
    public async Task<SpecialtySecretDto> ChangeSpecialtySecretAsync(int specId)
    {
        userContext.AssumeAuthenticated<BasicAuthenticatedUser>()
            .EnsureUserHasPermissions(UrlsClaims.Types.Specialty, UrlsClaims.Values.ChangeSecret);

        var specialtyToUpdate = await repo.GetSpecialtyAsync(specId);
        specialtyToUpdate.Invite = Generator.CreateGroupInviteCode();
        var updatedSpecialty = await repo.UpdateSpecialtyAsync(specialtyToUpdate);

        return new SpecialtySecretDto
        {
            Invite = updatedSpecialty.Invite
        };
    }
}
