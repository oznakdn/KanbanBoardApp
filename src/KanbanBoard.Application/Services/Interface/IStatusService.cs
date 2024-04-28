using KanbanBoard.Application.Dtos.StatusDtos;

namespace KanbanBoard.Application.Services.Interface;

public interface IStatusService
{
    Task<IEnumerable<GetStatusDto>> GetStatusByBoardIdAsync(string boardId, CancellationToken cancellationToken = default(CancellationToken));
    Task<GetStatusDto>GetStatusByIdAsync(string id,CancellationToken cancellationToken = default(CancellationToken));
}
