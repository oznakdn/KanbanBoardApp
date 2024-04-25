

using KanbanBoard.Core.Abstracts;

namespace KanbanBoard.Core.Models;

// To do - In Progress - Review - Done
public class Status : AbstractModel
{
    public string Name { get; set; }

    public string BoardId { get; set; }
    public Board? Board { get; set; }

    public ICollection<Issue>Issues { get; set; } = new HashSet<Issue>();

}
