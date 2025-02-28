using URLS.Groups.Features.Posts.Dtos;

namespace URLS.Groups.Features.Posts.Services;

public class CreatePostOrchestrator
{
    public async Task<PostDto> CreatePostAsync(CreatePostRequest request)
    {
        return new PostDto();
    }
}
