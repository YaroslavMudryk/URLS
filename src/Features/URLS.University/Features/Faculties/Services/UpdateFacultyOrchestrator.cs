using FluentValidation;
using URLS.Shared.Auth;
using URLS.University.Features.Faculties.DataAccess;
using URLS.University.Features.Faculties.Dtos;

namespace URLS.University.Features.Faculties.Services;

public class UpdateFacultyOrchestrator(
    IUserContext userContext,
    IValidator<CreateFacultyRequest> validator,
    FacultiesRepo repo)
{
    public async Task<FacultyDto> UpdateFacultyAsync(int facultyId, CreateFacultyRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        userContext.AssumeAuthenticated<BasicAuthenticatedUser>()
            .EnsureUserHasPermissions(UrlsClaims.Types.Faculty, UrlsClaims.Values.Update);

        await validator.ValidateAndThrowAsync(request);

        var facultyToUpdate = await repo.GetFacultyAsync(facultyId);
        request.Populate(facultyToUpdate);
        var updatedFaculty = await repo.UpdateTagAsync(facultyToUpdate);

        return updatedFaculty.MapToDto();
    }
}
