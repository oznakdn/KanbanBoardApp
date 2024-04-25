

using KanbanBoard.Core.Abstracts;

namespace KanbanBoard.Core.Models;

public class Comment : AbstractModel
{
    public string Text { get; set; }
    public string IssueId { get; set; }
    public string UserId { get; set; }


    public Issue? Issue { get; set; }
    public User? User { get; set; }

}
