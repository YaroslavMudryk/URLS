﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using URLS.Application.Extensions;
using URLS.Application.Helpers;
using URLS.Application.Services.Interfaces;
using URLS.Application.ViewModels;
using URLS.Application.ViewModels.Post;
using URLS.Constants.APIResponse;
using URLS.Constants.Extensions;
using URLS.Domain.Models;
using URLS.Infrastructure.Data.Context;

namespace URLS.Application.Services.Implementations
{
    public class PostService : IPostService
    {
        private readonly URLSDbContext _db;
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;
        private readonly IPermissionPostService _permissionPostService;
        private readonly IReactionService _reactionService;
        private readonly ICommonService _commonService;
        public PostService(URLSDbContext db, IMapper mapper, IIdentityService identityService, IPermissionPostService permissionPostService, IReactionService reactionService, ICommonService commonService)
        {
            _db = db;
            _mapper = mapper;
            _identityService = identityService;
            _permissionPostService = permissionPostService;
            _reactionService = reactionService;
            _commonService = commonService;
        }

        public async Task<Result<PostViewModel>> CreatePostAsync(PostCreateModel model)
        {
            if (!await _db.Groups.AnyAsync(s => s.Id == model.GroupId))
                return Result<PostViewModel>.NotFound(typeof(Group).NotFoundMessage(model.GroupId));

            if (!await _permissionPostService.CanCreatePostAsync(model.GroupId))
                return Result<PostViewModel>.Forbiden();

            if (!model.IsAvailableReactions)
                model.AvailableReactionIds = null;

            if (model.IsAvailableReactions)
                if (!ReactionHelper.ValidateReactionIds(model.AvailableReactionIds))
                    return Result<PostViewModel>.Error("Reactions not valid");

            var newPost = new Post
            {
                Title = model.Title,
                Content = model.Content,
                AvailableToComment = model.AvailableToComment,
                IsImportant = model.IsImportant,
                IsPublic = model.IsPublic,
                GroupId = model.GroupId,
                AvailableReactionIds = model.AvailableReactionIds,
                IsAvailableReactions = model.IsAvailableReactions,
                UserId = _identityService.GetUserId()
            };
            newPost.PrepareToCreate(_identityService);
            await _db.Posts.AddAsync(newPost);
            await _db.SaveChangesAsync();
            return Result<PostViewModel>.Created(_mapper.Map<PostViewModel>(newPost));
        }

        public async Task<Result<List<PostViewModel>>> GetPostsByGroupIdAsync(int groupId, int skip = 0, int count = 20)
        {
            var query = _db.Posts.AsNoTracking();

            if (await _permissionPostService.CanViewOnlyPublicPostsAsync(groupId))
                query = query.Where(s => s.IsPublic);

            query = query.Where(g => g.GroupId == groupId)
                .Include(g => g.User)
                .OrderByDescending(g => g.CreatedAt)
                .Skip(skip).Take(count);

            var posts = await query.ToListAsync();

            var postsToView = _mapper.Map<List<PostViewModel>>(posts);

            postsToView.ForEach(async post =>
            {
                var request = await _reactionService.GetAllReactionsByPostIdAsync(post.Id);
                post.Statistics = request.IsSuccess ? request.Data : null;
            });

            var totalCount = await _commonService.CountAsync<Post>(g => g.GroupId == groupId);

            return Result<List<PostViewModel>>.SuccessList(postsToView, Meta.FromMeta(totalCount, skip, count));
        }

        public async Task<Result<PostViewModel>> GetPostByIdAsync(int postId, int groupId)
        {
            var post = await _db.Posts.AsNoTracking().Include(s => s.User).FirstOrDefaultAsync(x => x.Id == postId);
            if (post == null)
                return Result<PostViewModel>.NotFound(typeof(Post).NotFoundMessage(postId));

            if (!_permissionPostService.CanViewPost(post))
                return Result<PostViewModel>.Forbiden();

            if (post.GroupId != groupId)
                return Result<PostViewModel>.NotFound("This post not from this group");

            var postToView = _mapper.Map<PostViewModel>(post);
            postToView.CountComments = await _db.Comments.CountAsync(s => s.PostId == postId);
            postToView.Statistics = (await _reactionService.GetAllReactionsByPostIdAsync(postId)).Data;

            return Result<PostViewModel>.SuccessWithData(postToView);
        }

        public async Task<Result<bool>> RemovePostAsync(int postId, int groupId)
        {
            var postToDelete = await _db.Posts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == postId);
            if (postToDelete == null)
                return Result<bool>.NotFound(typeof(Post).NotFoundMessage(postId));

            if (!await _permissionPostService.CanRemovePostAsync(groupId, postToDelete))
                return Result<bool>.Forbiden();

            if (postToDelete.GroupId != groupId)
                return Result<bool>.NotFound("This group don`t have current post");

            _db.Posts.Remove(postToDelete);
            await _db.SaveChangesAsync();
            return Result<bool>.Success();
        }

        public async Task<Result<PostViewModel>> UpdatePostAsync(PostEditModel model)
        {
            var postToUpdate = await _db.Posts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == model.Id);
            if (postToUpdate == null)
                return Result<PostViewModel>.NotFound("Post not found");

            if (!await _permissionPostService.CanUpdatePostAsync(model.GroupId, postToUpdate))
                return Result<PostViewModel>.Forbiden();

            if (!model.IsAvailableReactions)
                model.AvailableReactionIds = null;

            if (model.IsAvailableReactions)
                if (!ReactionHelper.ValidateReactionIds(model.AvailableReactionIds))
                    return Result<PostViewModel>.Error("Reactions not valid");

            postToUpdate.Title = model.Title;
            postToUpdate.Content = model.Content;
            postToUpdate.AvailableToComment = model.AvailableToComment;
            postToUpdate.IsImportant = model.IsImportant;
            postToUpdate.IsPublic = model.IsPublic;
            postToUpdate.IsAvailableReactions = model.IsAvailableReactions;
            postToUpdate.AvailableReactionIds = model.AvailableReactionIds;
            postToUpdate.PrepareToUpdate(_identityService);

            _db.Posts.Update(postToUpdate);
            await _db.SaveChangesAsync();

            return Result<PostViewModel>.SuccessWithData(_mapper.Map<PostViewModel>(postToUpdate));
        }
    }
}