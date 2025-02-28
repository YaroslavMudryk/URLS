using URLS.Groups.Features.Comments.Dtos;

namespace URLS.Groups.Features.Comments.Services;

public class CreateCommentOrchestrator
{
    public async Task<CommentDto> CreateCommentAsync(CreateCommentRequest request)
    {
        return new CommentDto();
    }
}
