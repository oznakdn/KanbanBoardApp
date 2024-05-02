namespace KanbanBoard.Application.Dtos.CommentDtos;

public class CreateCommentDto
{
    public string IssueId { get; set; }
    public string UserId { get; set; }
    public string Text { get; set; }

}
