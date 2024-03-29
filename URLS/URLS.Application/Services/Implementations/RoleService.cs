﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using URLS.Application.Extensions;
using URLS.Application.Helpers;
using URLS.Application.Services.Interfaces;
using URLS.Application.ViewModels;
using URLS.Application.ViewModels.RoleClaim;
using URLS.Domain.Models;
using URLS.Infrastructure.Data.Context;

namespace URLS.Application.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly URLSDbContext _db;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
        private readonly IIdentityService _identityService;
        private readonly INotificationService _notificationService;
        private readonly ICommonService _commonService;
        public RoleService(URLSDbContext db, IMapper mapper, IIdentityService identityService, IClaimService claimService, INotificationService notificationService, ICommonService commonService)
        {
            _db = db;
            _mapper = mapper;
            _identityService = identityService;
            _claimService = claimService;
            _notificationService = notificationService;
            _commonService = commonService;
        }

        public async Task<Result<RoleViewModel>> CreateRoleAsync(RoleCreateModel model)
        {
            if (await _commonService.IsExistAsync<Role>(s => s.Name == model.Name && s.ClaimsHash == model.ClaimsIds.GetHashForClaimIds()))
            {
                return Result<RoleViewModel>.Error("Same role already exist");
            }

            if (!await CheckClaimsAsync(model.ClaimsIds))
            {
                return Result<RoleViewModel>.Error("Not all claims exist");
            }

            var newRole = new Role
            {
                Name = model.Name,
                CanDelete = true,
                CountClaims = model.ClaimsIds.Count(),
                ClaimsHash = model.ClaimsIds.GetHashForClaimIds()
            };
            newRole.PrepareToCreate(_identityService);

            await _db.Roles.AddAsync(newRole);
            await _db.SaveChangesAsync();

            var roleClaims = model.ClaimsIds.Select(s => new RoleClaim
            {
                RoleId = newRole.Id,
                ClaimId = s
            }).ToList();

            roleClaims.ForEach(x =>
            {
                x.PrepareToCreate(_identityService);
            });

            await _db.RoleClaims.AddRangeAsync(roleClaims);
            await _db.SaveChangesAsync();

            return Result<RoleViewModel>.Created(_mapper.Map<RoleViewModel>(newRole));
        }

        public async Task<Result<List<RoleViewModel>>> GetAllRolesAsync()
        {
            var roles = await _db.Roles.AsNoTracking().ToListAsync();

            var rolesToView = _mapper.Map<List<RoleViewModel>>(roles);

            return Result<List<RoleViewModel>>.SuccessList(rolesToView);
        }

        public async Task<Result<RoleViewModel>> GetRoleByIdAsync(int id, bool withClaims)
        {
            var query = await _commonService.IsExistWithResultsAsync<Role>(s => s.Id == id);
            if (!query.IsExist)
                return Result<RoleViewModel>.NotFound("Role not found");

            var role = query.Results.First();

            var roleToView = _mapper.Map<RoleViewModel>(role);

            if (withClaims)
            {
                var res = await _claimService.GetClaimsByRoleIdAsync(role.Id);
                if (res.IsSuccess)
                    roleToView.Claims = res.Data;
            }
            return Result<RoleViewModel>.SuccessWithData(roleToView);
        }

        public async Task<Result<RoleViewModel>> RemoveRoleAsync(int roleId)
        {
            var query = await _commonService.IsExistWithResultsAsync<Role>(s => s.Id == roleId);
            if (!query.IsExist)
                return Result<RoleViewModel>.NotFound("Role not found");

            var roleToRemove = query.Results.First();

            if (!roleToRemove.CanDelete)
                return Result<RoleViewModel>.Error("This role can`t remove");

            var roleClaims = await _db.RoleClaims.Where(s => s.RoleId == roleId).ToListAsync();
            if (roleClaims.Any())
            {
                _db.RoleClaims.RemoveRange(roleClaims);
                await _db.SaveChangesAsync();
            }
            _db.Roles.Remove(roleToRemove);
            await _db.SaveChangesAsync();
            return Result<RoleViewModel>.Success();
        }

        public async Task<Result<RoleViewModel>> UpdateRoleAsync(RoleEditModel model)
        {
            var query = await _commonService.IsExistWithResultsAsync<Role>(s => s.Id == model.Id);
            if (!query.IsExist)
                return Result<RoleViewModel>.NotFound("Role not found");

            var roleToUpdate = query.Results.First();

            if (!CheckIsNeedToUpdateRole(roleToUpdate, model))
                return Result<RoleViewModel>.SuccessWithData(_mapper.Map<RoleViewModel>(roleToUpdate));

            var isAllClaimsExist = await CheckClaimsAsync(model.ClaimsIds);
            if (!isAllClaimsExist)
            {
                return Result<RoleViewModel>.Error("Not all claims exist");
            }

            var roleClaims = await _db.RoleClaims.AsNoTracking().Where(s => s.RoleId == model.Id).ToListAsync();

            _db.RoleClaims.RemoveRange(roleClaims);
            await _db.SaveChangesAsync();

            var newRoleClaims = model.ClaimsIds.Select(x => new RoleClaim
            {
                ClaimId = x,
                RoleId = model.Id,
            }).ToList();

            newRoleClaims.ForEach(s =>
            {
                s.PrepareToCreate(_identityService);
            });

            roleToUpdate.CountClaims = newRoleClaims.Count;
            roleToUpdate.ClaimsHash = model.ClaimsIds.GetHashForClaimIds();

            await _db.RoleClaims.AddRangeAsync(newRoleClaims);
            await _db.SaveChangesAsync();

            var userIds = await _db.UserRoles.AsNoTracking().Where(s => s.RoleId == model.Id).Select(s => s.UserId).ToListAsync();

            var res = await _notificationService.SendNotifyToUsersAsync(NotificationsHelper.GetChangePermissionNotification(), userIds);

            return Result<RoleViewModel>.SuccessWithData(_mapper.Map<RoleViewModel>(roleToUpdate));
        }

        private async Task<bool> CheckClaimsAsync(int[] claimsIds)
        {
            var claims = await _db.Claims.AsNoTracking().Where(s => claimsIds.Contains(s.Id)).ToListAsync();
            return claims.Count == claimsIds.Count();
        }

        private bool CheckIsNeedToUpdateRole(Role currentRole, RoleEditModel updatedRole)
        {
            var countOfNewRoleClaims = updatedRole.ClaimsIds.Count();
            var hash = updatedRole.ClaimsIds.GetHashForClaimIds();
            var name = updatedRole.Name;
            return countOfNewRoleClaims != currentRole.CountClaims
                || hash != currentRole.ClaimsHash
                || currentRole.Name != name;
        }
    }
}