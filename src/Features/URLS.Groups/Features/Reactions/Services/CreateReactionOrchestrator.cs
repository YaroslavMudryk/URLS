using URLS.Groups.Features.Reactions.Dtos;

namespace URLS.Groups.Features.Reactions.Services;

public class CreateReactionOrchestrator
{
    public async Task<ReactionDto> CreateReactionAsync(CreateReactionRequest request)
    {
        return new ReactionDto();
    }
}
