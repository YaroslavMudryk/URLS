using URLS.Groups.Features.Posts.Dtos;

namespace URLS.Groups.Features.Posts.Services;

public class GetPostsOrchestrator
{
    public async Task<PostsResponse> GetPostsAsync(int groupId)
    {
        return new PostsResponse();
    }
}
