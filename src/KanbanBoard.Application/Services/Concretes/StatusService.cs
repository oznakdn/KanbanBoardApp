using KanbanBoard.Application.Dtos.IssueDtos;
using KanbanBoard.Application.Dtos.StatusDtos;
using KanbanBoard.Application.Services.Interface;
using KanbanBoard.Infrastructure.Repositories.Manager;

namespace KanbanBoard.Application.Services.Concretes;

public class StatusService : IStatusService
{
    private readonly IRepositoryManager _repository;

    public StatusService(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<GetStatusDto>> GetStatusByBoardIdAsync(string boardId, CancellationToken cancellationToken = default)
    {
        var statuses = await _repository.Status.FindAllAsync(cancellationToken, filter: x => x.BoardId == boardId, includes: x => x.Issues);

        return statuses.OrderBy(x => x.CreatedDate).Select(x => new GetStatusDto
        {
            Id = x.Id,
            Name = x.Name,
            BoardId = x.BoardId,
            Issues = x.Issues.OrderBy(x => x.Order).Select(y => new GetIssueDto
            {
                Id = y.Id,
                StatusId = y.StatusId,
                Summary = y.Summary,
                Description = y.Description,
                IssueType = y.IssueType,
                Priority = y.Priority,
                Order = y.Order
            }).ToList()
        }).ToList();
    }

}
