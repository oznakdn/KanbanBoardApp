namespace KanbanBoard.Application.Dtos.IssueDtos;

public class UpdateIssueStatusDto
{
    public string IssueId { get; set; }
    public string StatusId { get; set; }
    public int OldOrder { get; set; }
    public int NewOrder { get; set; }

}
