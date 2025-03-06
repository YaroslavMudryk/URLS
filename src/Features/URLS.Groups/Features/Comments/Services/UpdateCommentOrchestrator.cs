using URLS.Groups.Features.Comments.Dtos;

namespace URLS.Groups.Features.Comments.Services;

public class UpdateCommentOrchestrator
{
    public async Task<CommentDto> UpdateCommentAsync(long commentId, CreateCommentRequest request)
    {
        return new CommentDto();
    }
}
