using URLS.Groups.Features.Comments.Dtos;

namespace URLS.Groups.Features.Comments.Services;

public class GetCommentsOrchestrator
{
    public async Task<CommentsResponse> GetCommentsAsync(int postId)
    {
        return new CommentsResponse();
    }
}
