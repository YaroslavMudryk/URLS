namespace URLS.Identity.Features.Apps.Dtos;

public class CreateAppRequest
{
    public string Name { get; set; }
    public string ShortName { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime? ActiveFrom { get; set; }
    public DateTime? ActiveTo { get; set; }
    public IReadOnlyList<int> ClaimIds { get; set; } = [];
}
