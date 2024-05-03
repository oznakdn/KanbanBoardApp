using KanbanBoard.Core.Abstracts;

namespace KanbanBoard.Core.Models;

public class UserTitle : AbstractModel
{
    public string Title { get; set; }
    public ICollection<User> Users { get; set; } = new HashSet<User>();
}
