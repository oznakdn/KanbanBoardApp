using KanbanBoard.Application.Dtos.StatusDtos;

namespace KanbanBoard.Application.Services.Interface;

public interface IStatusService
{
    Task CreateStatusAsync(CreateStatusDto createStatus, CancellationToken cancellationToken = default(CancellationToken));
    Task<IEnumerable<GetStatusDto>> GetStatusByBoardIdAsync(string boardId, CancellationToken cancellationToken = default(CancellationToken));
    Task<GetStatusDto>GetStatusByIdAsync(string id,CancellationToken cancellationToken = default(CancellationToken));

}
