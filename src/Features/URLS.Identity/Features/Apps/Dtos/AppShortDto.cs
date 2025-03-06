namespace URLS.Identity.Features.Apps.Dtos;

public class AppShortDto
{
    public int Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public bool IsActive { get; set; }
}
