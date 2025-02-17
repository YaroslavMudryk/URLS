using URLS.Groups.Features.Posts.Dtos;

namespace URLS.Groups.Features.Posts.Services;

public class UpdatePostOrchestrator
{
    public async Task<PostDto> UpdatePostAsync(int postId, CreatePostRequest request)
    {
        return new PostDto();
    }
}
