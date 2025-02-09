using URLS.Data.Audit;

namespace URLS.Data.Entities;

public class Faculty : AuditableBaseModelWithIdentity<int>
{
    public string Name { get; set; }
    public string NameEng { get; set; }
    public int Order { get; set; }
    public int UniversityId { get; set; }
    public University University { get; set; }
}
