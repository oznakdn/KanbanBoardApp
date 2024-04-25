using Microsoft.AspNetCore.Identity;

namespace KanbanBoard.Core.Models;

public class User : IdentityUser<string>
{
    public ICollection<UserIssues>UserIssues{ get; set; } = new HashSet<UserIssues>();
    public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

}
