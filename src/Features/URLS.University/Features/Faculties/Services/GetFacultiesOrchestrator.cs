using URLS.Shared.Auth;
using URLS.University.Features.Faculties.DataAccess;
using URLS.University.Features.Faculties.Dtos;

namespace URLS.University.Features.Faculties.Services;

public class GetFacultiesOrchestrator(
    IUserContext userContext,
    IFacultiesQuery query)
{
    public async Task<FacultiesResponse> GetFacultiesAsync(int universityId)
    {
        userContext.AssumeAuthenticated<BasicAuthenticatedUser>()
            .EnsureUserHasPermissions(UrlsClaims.Types.Faculty, UrlsClaims.Values.ViewAll);

        return new FacultiesResponse
        {
            Faculties = await query.GetFacultiesAsync(universityId),
        };
    }
}
