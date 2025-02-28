using URLS.Groups.Features.GroupMembers.Dtos;

namespace URLS.Groups.Features.GroupMembers.Services;

public class ActionGroupMemberOrchestrator
{
    public async Task<GroupMemberDto> ActionGroupMemberAsync(int groupMemberId, ActionGroupMemberRequest request)
    {
        return new GroupMemberDto();
    }
}
