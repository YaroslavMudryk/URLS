using URLS.Groups.Features.Groups.DataAccess;
using URLS.Groups.Features.Groups.Dtos;
using URLS.Shared.Auth;

namespace URLS.Groups.Features.Groups.Services;

public class GetGroupOrchestrator(
    IUserContext userContext,
    GroupsQuery query)
{
    public async Task<FullGroupDto> GetGroupAsync(int groupId)
    {
        CheckUserPermissions(groupId);
        var group = await query.GetGroupByIdAsync(groupId);

        return group.MapToFullDto();
    }

    private void CheckUserPermissions(int groupId)
    {
        userContext.AssumeAuthenticated<BasicAuthenticatedUser>()
            .EnsureUserHasPermissions(UrlsClaims.Types.Group, UrlsClaims.Values.View);

    }
}
