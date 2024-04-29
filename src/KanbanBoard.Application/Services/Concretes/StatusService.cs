using KanbanBoard.Application.Dtos.IssueDtos;
using KanbanBoard.Application.Dtos.StatusDtos;
using KanbanBoard.Application.Services.Interface;
using KanbanBoard.Core.Models;
using KanbanBoard.Infrastructure.Repositories.Manager;

namespace KanbanBoard.Application.Services.Concretes;

public class StatusService : IStatusService
{
    private readonly IRepositoryManager _repository;

    public StatusService(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task CreateStatusAsync(CreateStatusDto createStatus, CancellationToken cancellationToken = default)
    {
        var status = new Status
        {
            BoardId = createStatus.BoardId,
            Name = createStatus.Name
        };

        _repository.Status.Insers(status);
        await _repository.SaveAsync(cancellationToken);
    }

    public async Task<string> DeleteStatusAsync(string id, CancellationToken cancellationToken = default)
    {
        var status = await _repository.Status.FindByIdAsync(id, cancellationToken);
        string boardId = status.BoardId;
        _repository.Status.Delete(status);
        await _repository.SaveAsync(cancellationToken);
        return boardId;
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

    public async Task<GetStatusDto> GetStatusByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var status = await _repository.Status.FindByIdAsync(id, cancellationToken, x => x.Board!);

        return new GetStatusDto
        {
            Id = status.Id,
            Name = status.Name,
            BoardId = status.BoardId
        };
    }
}
