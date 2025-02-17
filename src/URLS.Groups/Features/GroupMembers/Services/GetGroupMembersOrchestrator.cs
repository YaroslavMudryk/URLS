using URLS.Groups.Features.GroupMembers.Dtos;

namespace URLS.Groups.Features.GroupMembers.Services;

public class GetGroupMembersOrchestrator
{
    public async Task<GroupMembersResponse> GetGroupMembersAsync(int groupId)
    {
        return new GroupMembersResponse();
    }
}
