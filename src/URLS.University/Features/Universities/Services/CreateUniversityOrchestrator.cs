using FluentValidation;
using URLS.Shared.Auth;
using URLS.University.Features.Universities.DataAccess;
using URLS.University.Features.Universities.Dtos;

namespace URLS.University.Features.Universities.Services;

public class CreateUniversityOrchestrator(
    IUserContext userContext,
    IValidator<CreateUniversityRequest> validator,
    UniversityRepo repo)
{
    public async Task<UniversityDto> CreateUniversityAsync(CreateUniversityRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var currentUser = userContext.AssumeAuthenticated<BasicAuthenticatedUser>();
        currentUser.EnsureUserHasPermissions(UrlsClaims.Types.University, UrlsClaims.Types.University);
        await validator.ValidateAndThrowAsync(request);
        var universityToCreate = request.MapToEntity();
        var createdUniversity = await repo.AddUniversityAsync(universityToCreate);

        return createdUniversity.MapToDto();
    }
}
