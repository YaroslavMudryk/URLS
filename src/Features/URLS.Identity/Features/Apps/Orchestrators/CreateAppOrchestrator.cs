using FluentValidation;
using URLS.Data.Entities;
using URLS.Identity.Features.Apps.DataAccess;
using URLS.Identity.Features.Apps.Dtos;
using URLS.Shared.Auth;

namespace URLS.Identity.Features.Apps.Orchestrators;

public class CreateAppOrchestrator(
    IUserContext userContext,
    TimeProvider timeProvider,
    AppRepo repo,
    IValidator<CreateAppRequest> validator)
{
    public async Task<CreatedAppResponse> CreateAppAsync(CreateAppRequest request)
    {
        var currentUser = userContext.AssumeAuthenticated<BasicAuthenticatedUser>();
        currentUser.EnsureUserHasPermissions(UrlsClaims.Types.App, UrlsClaims.Values.Create);

        await validator.ValidateAndThrowAsync(request);

        var appToCreate = BuildApp(request, currentUser.UserId);
        var createdApp = await repo.AddAppAsync(appToCreate);

        return MapResponse(createdApp, request);
    }

    private App BuildApp(CreateAppRequest request, int userId)
    {
        var utcNow = timeProvider.GetUtcNow().UtcDateTime;
        var appToCreate = request.MapToEntity();
        appToCreate.UpdatedAt = appToCreate.CreatedAt = utcNow;
        appToCreate.UserId = userId;
        appToCreate.AppClaims = request.ClaimIds.Select(s => new AppClaim
        {
            ClaimId = s,
            CreatedAt = utcNow,
            UpdatedAt = utcNow
        }).ToList();

        return appToCreate;
    }

    private static CreatedAppResponse MapResponse(App app, CreateAppRequest request)
    {
        var response = app.MapToCreatedResponse();
        response.ClaimIds = request.ClaimIds;
        return response;
    }
}
