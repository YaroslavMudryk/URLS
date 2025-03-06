using URLS.Shared;

namespace URLS.Identity.Features.Apps.Dtos;

public class AppsResponse
{
    public IReadOnlyList<AppShortDto> Apps { get; set; } = [];
    public Meta Meta { get; set; }
}
