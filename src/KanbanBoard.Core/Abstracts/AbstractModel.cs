using KanbanBoard.Core.Interfaces;

namespace KanbanBoard.Core.Abstracts;

public abstract class AbstractModel  : IModel
{
    public string Id { get ; set ; } = Guid.NewGuid().ToString();
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
