using KanbanBoard.Core.Abstracts;
using KanbanBoard.Core.Enums;

namespace KanbanBoard.Core.Models;

public class Issue : AbstractModel
{
    public string Summary { get; set; }
    public string? Description { get; set; }
    public PriorityType Priority { get; set; }
    public IssueType IssueType { get; set; }
    public int Order { get; set; }

    public string StatusId { get; set; }
    public Status? Status { get; set; }
    public ICollection<UserIssues> UserIssues { get; set; } = new HashSet<UserIssues>(); // Issue atanan userlar (Assignees)
    public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>(); // Issue'ya ait yorumlar

}
