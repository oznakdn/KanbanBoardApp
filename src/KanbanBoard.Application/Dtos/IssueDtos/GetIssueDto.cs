using KanbanBoard.Core.Enums;

namespace KanbanBoard.Application.Dtos.IssueDtos;

public class GetIssueDto
{
    public string Id { get; set; }
    public string StatusId { get; set; }
    public string Summary { get; set; }
    public string? Description { get; set; }
    public PriorityType Priority { get; set; }
    public IssueType IssueType { get; set; }
    public int Order { get; set; }
}
