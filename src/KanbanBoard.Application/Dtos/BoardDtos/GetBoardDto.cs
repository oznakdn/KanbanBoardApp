using KanbanBoard.Application.Dtos.StatusDtos;

namespace KanbanBoard.Application.Dtos.BoardDtos;

public class GetBoardDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    //public List<GetStatusDto> Status { get; set; } = new List<GetStatusDto>();

}
