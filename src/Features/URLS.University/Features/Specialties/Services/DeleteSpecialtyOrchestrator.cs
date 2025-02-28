using URLS.Shared.Auth;
using URLS.University.Features.Specialties.DataAccess;

namespace URLS.University.Features.Specialties.Services;

public class DeleteSpecialtyOrchestrator(
    IUserContext userContext,
    SpecialtyRepo repo)
{
    public async Task DeleteSpecialtyAsync(int specId)
    {
        userContext.AssumeAuthenticated<BasicAuthenticatedUser>()
            .EnsureUserHasPermissions(UrlsClaims.Types.Specialty, UrlsClaims.Values.Delete);

        await repo.DeleteSpecialtyAsync(specId);
    }
}
