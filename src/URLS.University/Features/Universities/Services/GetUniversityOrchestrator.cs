using URLS.Shared.Auth;
using URLS.University.Features.Universities.DataAccess;
using URLS.University.Features.Universities.Dtos;

namespace URLS.University.Features.Universities.Services;

public class GetUniversityOrchestrator(
    IUserContext userContext,
    UniversityQuery query)
{
    public async Task<UniversityDto> GetUniversityAsync(int id = 1)
    {
        userContext.AssumeAuthenticated<BasicAuthenticatedUser>()
            .EnsureUserHasPermissions(UrlsClaims.Types.University, UrlsClaims.Values.View);

        return await query.GetUniversityAsync(id);
    }
}
