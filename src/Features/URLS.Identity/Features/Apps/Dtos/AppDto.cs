namespace URLS.Identity.Features.Apps.Dtos;

public class AppDto
{
    public int Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public bool IsActive { get; set; }
    public DateTime? ActiveFrom { get; set; }
    public DateTime? ActiveTo { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public IReadOnlyList<ClaimDto> Claims { get; set; }
}
