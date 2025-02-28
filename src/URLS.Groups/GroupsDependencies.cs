using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using URLS.Groups.Features.Comments.DataAccess;
using URLS.Groups.Features.Comments.Dtos;
using URLS.Groups.Features.Comments.Dtos.Validators;
using URLS.Groups.Features.Comments.Services;
using URLS.Groups.Features.GroupMembers.DataAccess;
using URLS.Groups.Features.GroupMembers.Dtos;
using URLS.Groups.Features.GroupMembers.Dtos.Validators;
using URLS.Groups.Features.GroupMembers.Services;
using URLS.Groups.Features.Groups.DataAccess;
using URLS.Groups.Features.Groups.Dtos;
using URLS.Groups.Features.Groups.Dtos.Validators;
using URLS.Groups.Features.Groups.Services;
using URLS.Groups.Features.Intives.DataAccess;
using URLS.Groups.Features.Intives.Dtos;
using URLS.Groups.Features.Intives.Dtos.Validators;
using URLS.Groups.Features.Intives.Services;
using URLS.Groups.Features.Posts.DataAccess;
using URLS.Groups.Features.Posts.Dtos;
using URLS.Groups.Features.Posts.Dtos.Validators;
using URLS.Groups.Features.Posts.Services;
using URLS.Groups.Features.Reactions.DataAccess;
using URLS.Groups.Features.Reactions.Dtos;
using URLS.Groups.Features.Reactions.Dtos.Validators;
using URLS.Groups.Features.Reactions.Services;

namespace URLS.Groups;

public static class GroupsDependencies
{
    public static void Register(IServiceCollection services)
    {
        //Orchestrators
        services.AddScoped<GetCommentsOrchestrator>();
        services.AddScoped<CreateCommentOrchestrator>();
        services.AddScoped<UpdateCommentOrchestrator>();
        services.AddScoped<DeleteCommentOrchestrator>();

        services.AddScoped<GetGroupMembersOrchestrator>();
        services.AddScoped<CreateGroupMemberOrchestrator>();
        services.AddScoped<UpdateGroupMemberOrchestrator>();
        services.AddScoped<DeleteGroupMemberOrchestrator>();
        services.AddScoped<ActionGroupMemberOrchestrator>();

        services.AddScoped<GetGroupOrchestrator>();
        services.AddScoped<GetGroupsOrchestrator>();
        services.AddScoped<CreateGroupOrchestrator>();
        services.AddScoped<UpdateGroupOrchestrator>();
        services.AddScoped<DeleteGroupOrchestrator>();

        services.AddScoped<GetInvitesOrchestrator>();
        services.AddScoped<CreateInviteOrchestrator>();
        services.AddScoped<UpdateInviteOrchestrator>();
        services.AddScoped<DeleteInviteOrchestrator>();

        services.AddScoped<GetPostsOrchestrator>();
        services.AddScoped<CreatePostOrchestrator>();
        services.AddScoped<UpdatePostOrchestrator>();
        services.AddScoped<DeletePostOrchestrator>();

        services.AddScoped<GetReactionsOrchestrator>();
        services.AddScoped<CreateReactionOrchestrator>();
        services.AddScoped<UpdateReactionOrchestrator>();
        services.AddScoped<DeleteReactionOrchestrator>();

        //DataAccess
        services.AddScoped<CommentsRepo>();
        services.AddScoped<GroupsRepo>();
        services.AddScoped<InvitesRepo>();
        services.AddScoped<PostsRepo>();
        services.AddScoped<ReactionsRepo>();
        services.AddScoped<UserGroupsRepo>();

        services.AddScoped<GroupsQuery>();

        //Services


        //Validators
        services.AddScoped<IValidator<CreateCommentRequest>, CreateCommentRequestValidator>();
        services.AddScoped<IValidator<CreateGroupRequest>, CreateGroupRequestValidator>();
        services.AddScoped<IValidator<CreateInviteRequest>, CreateInviteRequestValidator>();
        services.AddScoped<IValidator<CreatePostRequest>, CreatePostRequestValidator>();
        services.AddScoped<IValidator<CreateReactionRequest>, CreateReactionRequestValidator>();
        services.AddScoped<IValidator<CreateGroupMemberRequest>, CreateGroupMemberRequestValidator>();
    }
}
