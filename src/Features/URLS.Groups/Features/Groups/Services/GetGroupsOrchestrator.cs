using URLS.Groups.Features.Groups.Dtos;

namespace URLS.Groups.Features.Groups.Services;

public class GetGroupsOrchestrator
{
    public async Task<GroupsResponse> GetGroupsAsync()
    {
        return new GroupsResponse();
    }
}
