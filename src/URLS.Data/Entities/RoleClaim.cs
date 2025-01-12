using URLS.Data.Audit;

namespace URLS.Data.Entities;

public class RoleClaim : AuditableBaseModelWithIdentity<int>
{
    public int RoleId { get; set; }
    public Role Role { get; set; }
    
    public int ClaimId { get; set; }
    public Claim Claim { get; set; }
}
