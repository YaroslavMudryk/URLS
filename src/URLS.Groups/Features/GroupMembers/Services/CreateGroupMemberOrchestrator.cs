using URLS.Groups.Features.GroupMembers.Dtos;

namespace URLS.Groups.Features.GroupMembers.Services;

public class CreateGroupMemberOrchestrator
{
    public async Task<GroupMemberDto> CreateGroupMemberAsync(CreateGroupMemberRequest request)
    {
        return new GroupMemberDto();
    }
}
