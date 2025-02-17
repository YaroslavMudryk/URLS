using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using URLS.Groups.Features.Comments.Dtos;
using URLS.Groups.Features.Comments.Services;
using URLS.Groups.Features.GroupMembers.Dtos;
using URLS.Groups.Features.GroupMembers.Services;
using URLS.Groups.Features.Groups.Dtos;
using URLS.Groups.Features.Groups.Services;
using URLS.Groups.Features.Intives.Dtos;
using URLS.Groups.Features.Intives.Services;
using URLS.Groups.Features.Posts.Dtos;
using URLS.Groups.Features.Posts.Services;
using URLS.Groups.Features.Reactions.Dtos;
using URLS.Groups.Features.Reactions.Services;
using URLS.Shared.Api;

namespace URLS.Groups;

public static class GroupsEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/api/v1/groups",
            async (GetGroupsOrchestrator orc) =>
            {
                var groupsResponse = await orc.GetGroupsAsync();
                return Results.Ok(groupsResponse);
            })
            .Produces<ApiResponse<GroupsResponse>>();

        app.MapGet("/api/v1/groups/{groupId:int}",
            async (int groupId, GetGroupOrchestrator orc) =>
            {
                var groupDto = await orc.GetGroupAsync(groupId);
                return Results.Ok(groupDto);
            })
            .Produces<ApiResponse<FullGroupDto>>();

        app.MapPost("/api/v1/groups",
            async (CreateGroupRequest request, CreateGroupOrchestrator orc) =>
            {
                var createdGroup = await orc.CreateGroupAsync(request);
                return Results.Json(createdGroup, statusCode: 201);
            })
            .Produces<ApiResponse<GroupDto>>();

        app.MapPut("/api/v1/groups/{groupId:int}",
            async (int groupId, CreateGroupRequest request, UpdateGroupOrchestrator orc) =>
            {
                var updatedGroup = await orc.UpdateGroupAsync(groupId, request);
                return Results.Ok(updatedGroup);
            })
            .Produces<ApiResponse<GroupDto>>();

        app.MapDelete("/api/v1/groups/{groupId:int}",
            async (int groupId, DeleteGroupOrchestrator orc) =>
            {
                await orc.DeleteGroupAsync(groupId);
                return Results.NoContent();
            });

        app.MapGet("/api/v1/groups/{groupId:int}/invites",
            async (int groupId, GetInvitesOrchestrator orc) =>
            {
                var invitesResponse = await orc.GetInvitesAsync(groupId);
                return Results.Ok(invitesResponse);
            })
            .Produces<ApiResponse<InvitesResponse>>();

        app.MapPost("/api/v1/invites",
            async (CreateInviteRequest request, CreateInviteOrchestrator orc) =>
            {
                var createdInvite = await orc.CreateInviteAsync(request);
                return Results.Json(createdInvite, statusCode: 201);
            })
            .Produces<ApiResponse<InviteDto>>();

        app.MapPut("/api/v1/invites/{inviteId:int}",
            async (int inviteId, CreateInviteRequest request, UpdateInviteOrchestrator orc) =>
            {
                var updatedInvite = await orc.UpdateInviteAsync(inviteId, request);
                return Results.Ok(updatedInvite);
            })
            .Produces<ApiResponse<InviteDto>>();

        app.MapDelete("/api/v1/invites/{inviteId:int}",
            async (int inviteId, DeleteInviteOrchestrator orc) =>
            {
                await orc.DeleteInviteAsync(inviteId);
                return Results.NoContent();
            });

        app.MapGet("/api/v1/groups/{groupId:int}/posts",
            async (int groupId, GetPostsOrchestrator orc) =>
            {
                var postsResponse = await orc.GetPostsAsync(groupId);
                return Results.Ok(postsResponse);
            })
            .Produces<ApiResponse<PostsResponse>>();

        app.MapPost("/api/v1/posts",
            async (CreatePostRequest request, CreatePostOrchestrator orc) =>
            {
                var createdPost = await orc.CreatePostAsync(request);
                return Results.Json(createdPost, statusCode: 201);
            })
            .Produces<ApiResponse<PostDto>>();

        app.MapPut("/api/v1/posts/{postId:int}",
            async (int postId, CreatePostRequest request, UpdatePostOrchestrator orc) =>
            {
                var updatedPost = await orc.UpdatePostAsync(postId, request);
                return Results.Ok(updatedPost);
            })
            .Produces<ApiResponse<PostDto>>();

        app.MapDelete("/api/v1/posts/{postId:int}",
            async (int postId, DeletePostOrchestrator orc) =>
            {
                await orc.DeletePostAsync(postId);
                return Results.NoContent();
            });

        app.MapGet("/api/v1/posts/{postId:int}/reactions",
            async (int postId, GetReactionsOrchestrator orc) =>
            {
                var reactionsResponse = await orc.GetReactionsAsync(postId);
                return Results.Ok(reactionsResponse);
            })
            .Produces<ApiResponse<ReactionsResponse>>();

        app.MapPost("/api/v1/reactions",
            async (CreateReactionRequest request, CreateReactionOrchestrator orc) =>
            {
                var createdReaction = await orc.CreateReactionAsync(request);
                return Results.Json(createdReaction, statusCode: 201);
            })
            .Produces<ApiResponse<ReactionDto>>();

        app.MapPut("/api/v1/reactions/{reactionId:Guid}",
            async (Guid reactionId, CreateReactionRequest request, UpdateReactionOrchestrator orc) =>
            {
                var updatedReaction = await orc.UpdateReactionAsync(reactionId, request);
                return Results.Ok(updatedReaction);
            })
            .Produces<ApiResponse<ReactionDto>>();

        app.MapDelete("/api/v1/reactions/{reactionId:Guid}",
            async (Guid reactionId, DeleteReactionOrchestrator orc) =>
            {
                await orc.DeleteReactionAsync(reactionId);
                return Results.NoContent();
            });

        app.MapGet("/api/v1/posts/{postId:int}/comments",
            async (int postId, GetCommentsOrchestrator orc) =>
            {
                var commentsResponse = await orc.GetCommentsAsync(postId);
                return Results.Ok(commentsResponse);
            })
            .Produces<ApiResponse<CommentsResponse>>();

        app.MapPost("/api/v1/comments",
            async (CreateCommentRequest request, CreateCommentOrchestrator orc) =>
            {
                var createdComment = await orc.CreateCommentAsync(request);
                return Results.Json(createdComment, statusCode: 201);
            })
            .Produces<ApiResponse<CommentDto>>();

        app.MapPut("/api/v1/comments/{commentId:long}",
            async (long commentId, CreateCommentRequest request, UpdateCommentOrchestrator orc) =>
            {
                var updatedComment = await orc.UpdateCommentAsync(commentId, request);
                return Results.Ok(updatedComment);
            })
            .Produces<ApiResponse<CommentDto>>();

        app.MapDelete("/api/v1/comments/{commentId:long}",
            async (long commentId, DeleteCommentOrchestrator orc) =>
            {
                await orc.DeleteCommentAsync(commentId);
                return Results.NoContent();
            });

        app.MapGet("/api/v1/groups/{groupId:int}/members",
            async (int groupId, GetGroupMembersOrchestrator orc) =>
            {
                var groupMembersResponse = await orc.GetGroupMembersAsync(groupId);
                return Results.Ok(groupMembersResponse);
            })
            .Produces<ApiResponse<GroupMembersResponse>>();

        app.MapPost("/api/v1/members",
            async (CreateGroupMemberRequest request, CreateGroupMemberOrchestrator orc) =>
            {
                var createdGroupMember = await orc.CreateGroupMemberAsync(request);
                return Results.Json(createdGroupMember, statusCode: 201);
            })
            .Produces<ApiResponse<GroupMemberDto>>();

        app.MapPost("/api/v1/members/{memberId:int}/action",
            async (int memberId, ActionGroupMemberRequest request, ActionGroupMemberOrchestrator orc) =>
            {
                var actionedGroupMember = await orc.ActionGroupMemberAsync(memberId, request);
                return Results.Ok(actionedGroupMember);
            })
            .Produces<ApiResponse<GroupMemberDto>>();

        app.MapPut("/api/v1/members/{memberId:int}",
            async (int memberId, CreateGroupMemberRequest request, UpdateGroupMemberOrchestrator orc) =>
            {
                var updatedGroupMember = await orc.UpdateGroupMemberAsync(memberId, request);
                return Results.Ok(updatedGroupMember);
            })
            .Produces<ApiResponse<GroupMemberDto>>();

        app.MapDelete("/api/v1/members/{memberId:int}",
            async (int memberId, DeleteGroupMemberOrchestrator orc) =>
            {
                await orc.DeleteGroupMemberAsync(memberId);
                return Results.NoContent();
            });
    }
}
