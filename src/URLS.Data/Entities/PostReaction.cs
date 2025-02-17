using URLS.Data.Audit;

namespace URLS.Data.Entities;

public class PostReaction : AuditableBaseModelWithIdentity<Guid>
{
    public int ReactionTypeId { get; set; }
    public int PostId { get; set; }
    public GroupPost Post { get; set; }
    public int? FromId { get; set; }
    public User From { get; set; }
}
