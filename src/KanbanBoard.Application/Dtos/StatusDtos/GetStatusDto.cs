using KanbanBoard.Application.Dtos.IssueDtos;

namespace KanbanBoard.Application.Dtos.StatusDtos;

public class GetStatusDto
{
    public string Id { get; set; } 
    public string Name { get; set; }
    public string BoardId { get; set; }
    public List<GetIssueDto> Issues { get; set; } = new List<GetIssueDto>();

}
