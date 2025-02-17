using URLS.Groups.Features.Groups.Dtos;

namespace URLS.Groups.Features.Groups.Services;

public class GetGroupOrchestrator
{
    public async Task<FullGroupDto> GetGroupAsync(int groupId)
    {
        return new FullGroupDto();
    }
}
