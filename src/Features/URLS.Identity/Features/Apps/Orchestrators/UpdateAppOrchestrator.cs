using FluentValidation;
using URLS.Data.Entities;
using URLS.Identity.Features.Apps.DataAccess;
using URLS.Identity.Features.Apps.Dtos;
using URLS.Identity.Features.Apps.Services;
using URLS.Shared.Auth;

namespace URLS.Identity.Features.Apps.Orchestrators;

public class UpdateAppOrchestrator(
    IUserContext userContext,
    TimeProvider timeProvider,
    AppRepo repo,
    AppClaimsService appClaimsService,
    IValidator<UpdateAppRequest> validator)
{
    public async Task<UpdatedAppResponse> UpdateAppAsync(int appId, UpdateAppRequest request)
    {
        var currentUser = userContext.AssumeAuthenticated<BasicAuthenticatedUser>();
        currentUser.EnsureUserHasPermissions(UrlsClaims.Types.App, UrlsClaims.Values.Update);
        request.Id = appId;
        await validator.ValidateAndThrowAsync(request);
        await using var transaction = await repo.BeginTransactionAsync();
        var appToUpdate = await repo.GetAppAsync(appId);
        request.Populate(appToUpdate);
        appToUpdate.UpdatedAt = timeProvider.GetUtcNow().UtcDateTime;
        var updatedApp = await repo.UpdateAppAsyc(appToUpdate);
        await appClaimsService.UpdateClaimsForAppAsync(appId, request.ClaimIds);
        await transaction.CommitAsync();

        return MapResponse(updatedApp, request);
    }

    private static UpdatedAppResponse MapResponse(App app, UpdateAppRequest request)
    {
        var response = app.MapToUpdatedResponse();
        response.ClaimIds = request.ClaimIds;
        return response;
    }
}
