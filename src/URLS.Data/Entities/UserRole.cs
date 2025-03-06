using URLS.Data.Audit;

namespace URLS.Data.Entities;

public class UserRole : AuditableBaseModelWithIdentity<int>
{
    public int UserId { get; set; }
    public User User { get; set; }
    
    public int RoleId { get; set; }
    public Role Role { get; set; }
}
