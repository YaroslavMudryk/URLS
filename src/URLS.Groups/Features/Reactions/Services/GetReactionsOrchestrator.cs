using URLS.Groups.Features.Reactions.Dtos;

namespace URLS.Groups.Features.Reactions.Services;

public class GetReactionsOrchestrator
{
    public async Task<ReactionsResponse> GetReactionsAsync(int postId)
    {
        return new ReactionsResponse();
    }
}
