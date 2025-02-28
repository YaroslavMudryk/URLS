using URLS.Groups.Features.Reactions.Dtos;

namespace URLS.Groups.Features.Reactions.Services;

public class UpdateReactionOrchestrator
{
    public async Task<ReactionDto> UpdateReactionAsync(Guid reactionId, CreateReactionRequest request)
    {
        return new ReactionDto();
    }
}
