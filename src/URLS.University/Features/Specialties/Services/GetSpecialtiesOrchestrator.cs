using URLS.Shared.Auth;
using URLS.University.Features.Specialties.DataAccess;
using URLS.University.Features.Specialties.Dtos;

namespace URLS.University.Features.Specialties.Services;

public class GetSpecialtiesOrchestrator(
    IUserContext userContext,
    ISpecialtiesQuery query)
{
    public async Task<SpecialtiesResponse> GetSpecialtiesAsync(int facultyId)
    {
        userContext.AssumeAuthenticated<BasicAuthenticatedUser>()
            .EnsureUserHasPermissions(UrlsClaims.Types.Specialty, UrlsClaims.Values.View);

        return new SpecialtiesResponse
        {
            Specialties = await query.GetSpecialtiesAsync(facultyId)
        };
    }
}
