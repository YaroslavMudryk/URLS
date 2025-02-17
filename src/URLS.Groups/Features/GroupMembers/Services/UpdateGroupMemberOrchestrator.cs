using URLS.Groups.Features.GroupMembers.Dtos;

namespace URLS.Groups.Features.GroupMembers.Services;

public class UpdateGroupMemberOrchestrator
{
    public async Task<GroupMemberDto> UpdateGroupMemberAsync(int groupMemberId, CreateGroupMemberRequest request)
    {
        return new GroupMemberDto();
    }
}
