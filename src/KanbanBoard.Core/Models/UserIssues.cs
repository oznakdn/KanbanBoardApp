using KanbanBoard.Core.Interfaces;

namespace KanbanBoard.Core.Models;

public class UserIssues : IModel
{
    public string Id { get ; set ; } = Guid.NewGuid().ToString();
    public string UserId { get; set; }  
    public string IssueId { get; set; }

    public User? User { get; set; }
    public Issue? Issue { get; set; }
}
