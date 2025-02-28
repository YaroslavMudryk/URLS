namespace URLS.University.Features.Specialties.Dtos;

public class CreateSpecialtyRequest
{
    public string Name { get; set; }
    public string NameEng { get; set; }
    public string Code { get; set; }
    public int? FacultyId { get; set; }
}
