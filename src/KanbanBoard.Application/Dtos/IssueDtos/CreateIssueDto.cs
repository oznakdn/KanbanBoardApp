using KanbanBoard.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace KanbanBoard.Application.Dtos.IssueDtos;

public class CreateIssueDto
{
    public string StatusId { get; set; }

    [Required]
    public string Summary { get; set; }
    public string? Description { get; set; }
    public int Priority { get; set; }
    public int IssueType { get; set; }

}
