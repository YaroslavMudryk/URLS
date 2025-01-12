using URLS.Data.Audit;

namespace URLS.Data.Entities;

public class AppClaim : AuditableBaseModelWithIdentity<int>
{
    public int AppId { get; set; }
    public App App { get; set; }
    
    public int ClaimId { get; set; }
    public Claim Claim { get; set; }
}
