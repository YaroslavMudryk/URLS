using URLS.Data.Audit;

namespace URLS.Data.Entities;

public class Specialty : AuditableBaseModelWithIdentity<int>
{
    public string Name { get; set; }
    public string NameEng { get; set; }
    public string Code { get; set; }
    public string Invite { get; set; }
    public int FacultyId { get; set; }
    public Faculty Faculty { get; set; }
    public List<Group> Groups { get; set; }
}
