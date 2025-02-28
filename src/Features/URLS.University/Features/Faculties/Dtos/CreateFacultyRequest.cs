namespace URLS.University.Features.Faculties.Dtos;

public class CreateFacultyRequest
{
    public string Name { get; set; }
    public string NameEng { get; set; }
    public int Order { get; set; }
    public int? UniversityId { get; set; }
}
