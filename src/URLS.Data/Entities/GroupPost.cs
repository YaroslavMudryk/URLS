using System.ComponentModel.DataAnnotations;
using URLS.Data.Audit;

namespace URLS.Data.Entities;

public class GroupPost : AuditableBaseModelWithIdentity<int>
{
    [Required, StringLength(150, MinimumLength = 1)]
    public string Title { get; set; }
    [Required, StringLength(10000, MinimumLength = 5)]
    public string Content { get; set; }
    [Required]
    public bool IsImportant { get; set; }
    [Required]
    public bool AvailableToComment { get; set; }
    [Required]
    public bool IsPublic { get; set; }
    public int[] AvailableReactionIds { get; set; }
    public bool IsAvailableReactions { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int GroupId { get; set; }
    public Group Group { get; set; }
    public List<PostComment> Comments { get; set; }
    public List<PostReaction> Reactions { get; set; }
}
