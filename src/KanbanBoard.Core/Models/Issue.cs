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

// Issue repository Insert metodu

/*
 
  var task = new Task()
    {
        ColumnId = columnId,
        Title = title,
        Description = description,
        Order = GetNextOrder(columnId) // Sıradaki order'ı alır
    };

    dbContext.Tasks.Add(task);
    dbContext.SaveChanges();
 
 
 */

/*
 * 
private int GetNextOrder(int columnId)
{
    // Veritabanından kolondaki son order'ı alır
    var lastOrder = dbContext.Tasks.Where(t => t.ColumnId == columnId).Max(t => t.Order) ?? 0;

    return lastOrder + 1; // Sonraki order'ı hesaplar
}
 
*/