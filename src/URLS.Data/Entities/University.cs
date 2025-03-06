using URLS.Data.Audit;

namespace URLS.Data.Entities;

public class University : AuditableBaseModelWithIdentity<int>
{
    public string Name { get; set; }
    public string ShortName { get; set; }
    public string NameEng { get; set; }
    public string ShortNameEng { get; set; }
    public List<Faculty> Faculties { get; set; }
}
