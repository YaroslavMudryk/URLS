using Microsoft.EntityFrameworkCore;
using URLS.Data;
using URLS.Data.Entities;
using URLS.Identity.Features.Apps.Dtos;

namespace URLS.Identity.Features.Apps.Services;

public class AppsEnricherService(UrlsContext urlsContext)
{
    public async Task<AppDto> GetEnrichedAppAsync(App app)
    {
        var appDto = app.MapToDto();
        appDto.Claims = await GetAppClaimsAsync(app.Id);
        return appDto;
    }

    private async Task<IReadOnlyList<ClaimDto>> GetAppClaimsAsync(int appId)
    {
        return await urlsContext.AppClaims.Where(s => s.AppId == appId)
            .Select(s => new ClaimDto
            {
                Id = s.ClaimId,
                CreatedAt = s.Claim.CreatedAt,
                UpdatedAt = s.Claim.UpdatedAt,
                Description = s.Claim.Description,
                Type = s.Claim.Type,
                Value = s.Claim.Value,
            }).ToListAsync();
    }
}
