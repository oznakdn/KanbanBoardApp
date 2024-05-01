namespace KanbanBoard.Application.Dtos.BoardDtos;

public class UpdateBoardDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
}
