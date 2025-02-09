using URLS.Shared.Api;

namespace URLS.University.Features.Universities.Dtos;

public class UniversityDto
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public string NameEng { get; set; }
    public string ShortNameEng { get; set; }
    public DateTime UpdatedAt { get; set; }
}
