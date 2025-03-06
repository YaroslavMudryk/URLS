using FluentValidation;
using URLS.Shared.Auth;
using URLS.University.Features.Faculties.DataAccess;
using URLS.University.Features.Faculties.Dtos;

namespace URLS.University.Features.Faculties.Services;

public class CreateFacultyOrchestrator(
    IUserContext userContext,
    FacultiesRepo repo,
    IValidator<CreateFacultyRequest> validator)
{
    public async Task<FacultyDto> CreateFacultyAsync(CreateFacultyRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        userContext.AssumeAuthenticated<BasicAuthenticatedUser>()
            .EnsureUserHasPermissions(UrlsClaims.Types.Faculty, UrlsClaims.Values.Create);

        await validator.ValidateAndThrowAsync(request);

        var facultyToCreate = request.MapToEntity();
        var createdFaculty = await repo.AddFacultyAsync(facultyToCreate);

        return createdFaculty.MapToDto();
    }
}
