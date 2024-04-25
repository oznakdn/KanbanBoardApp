using KanbanBoard.Application.Dtos.BoardDtos;

namespace KanbanBoard.Application.Services.Interface;

public interface IBoardService
{
    Task<IEnumerable<GetBoardDto>> GetBoardsAsync(CancellationToken cancellationToken = default(CancellationToken));
}
