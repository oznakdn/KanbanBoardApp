using Microsoft.AspNetCore.Identity;

namespace KanbanBoard.Core.Models;

public class User : IdentityUser
{
    public ICollection<UserIssues>UserIssues{ get; set; } = new HashSet<UserIssues>();
    public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    public string? ProfilePicture { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string TitleId { get; set; }
    public UserTitle? Title { get; set; }
    public string Fullname => $"{FirstName} {LastName}";

}
