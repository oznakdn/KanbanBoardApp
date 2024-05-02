namespace KanbanBoard.Application.Dtos.CommentDtos;

public class GetCommentDto
{
    public string Text { get; set; }
    public DateTime CommentDate { get; set; }

    public string Username { get; set; }
    public string? UserProfilePicture { get; set; }

}
