namespace KanbanBoard.Application.Dtos.IssueDtos;

public class GetIssueStatisticsDto
{
    public int TaskCount { get; set; } = 0;
    public int StoryCount { get; set; } = 0;
    public int BugCount { get; set; } = 0;

}
