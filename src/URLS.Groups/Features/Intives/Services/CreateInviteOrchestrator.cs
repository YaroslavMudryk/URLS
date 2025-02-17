using URLS.Groups.Features.Intives.Dtos;

namespace URLS.Groups.Features.Intives.Services;

public class CreateInviteOrchestrator
{
    public async Task<InviteDto> CreateInviteAsync(CreateInviteRequest request)
    {
        return new InviteDto();
    }
}
