using KanbanBoard.Core.Abstracts;

namespace KanbanBoard.Core.Models;

public class Board : AbstractModel
{
    public string Title { get; set; }
    public string? Description { get; set; }

    public ICollection<Status>Statuses { get; set; } = new HashSet<Status>(); // Todo - In Progress - Review - Done

}
