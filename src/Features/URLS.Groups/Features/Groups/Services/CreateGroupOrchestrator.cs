using URLS.Groups.Features.Groups.Dtos;

namespace URLS.Groups.Features.Groups.Services;

public class CreateGroupOrchestrator
{
    public async Task<GroupDto> CreateGroupAsync(CreateGroupRequest request)
    {
        return new GroupDto();
    }
}
