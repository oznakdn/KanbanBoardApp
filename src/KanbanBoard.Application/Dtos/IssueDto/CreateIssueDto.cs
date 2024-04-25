using KanbanBoard.Core.Enums;

namespace KanbanBoard.Application.Dtos.IssueDto;

public class CreateIssueDto
{
    public string StatusId { get; set; }
    public string Summary { get; set; }
    public string? Description { get; set; }
    public PriorityType Priority { get; set; }
    public IssueType IssueType { get; set; }
    public int Order { get; set; }

}
