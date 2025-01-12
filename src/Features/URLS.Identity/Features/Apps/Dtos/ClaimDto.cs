namespace URLS.Identity.Features.Apps.Dtos;

public class ClaimDto
{
    public int Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string Type { get; set; }
    public string Value { get; set; }
    public string Description { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
