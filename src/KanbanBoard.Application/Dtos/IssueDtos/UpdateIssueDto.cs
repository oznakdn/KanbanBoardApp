using KanbanBoard.Core.Enums;

namespace KanbanBoard.Application.Dtos.IssueDtos;

public class UpdateIssueDto
{
    public string Id { get; set; }
    public string StatusId { get; set; }
    public string Summary { get; set; }
    public string? Description { get; set; }
    public int Priority { get; set; }
    public int IssueType { get; set; }
}
