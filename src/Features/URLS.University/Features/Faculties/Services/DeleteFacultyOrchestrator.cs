using URLS.Shared.Auth;
using URLS.University.Features.Faculties.DataAccess;

namespace URLS.University.Features.Faculties.Services;

public class DeleteFacultyOrchestrator(
    IUserContext userContext,
    FacultiesRepo repo)
{
    public async Task DeleteFacultyAsync(int facultyId)
    {
        userContext.AssumeAuthenticated<BasicAuthenticatedUser>()
            .EnsureUserHasPermissions(UrlsClaims.Types.Faculty, UrlsClaims.Values.Delete);

        await repo.DeleteFacultyAsync(facultyId);
    }
}
