using KanbanBoard.Application.Dtos.BoardDtos;

namespace KanbanBoard.Application.Services.Interface;

public interface IBoardService
{
    Task CreateBoardAsync(CreateBoardDto createBoard, CancellationToken cancellationToken = default(CancellationToken));
    Task UpdateBoardAsync(UpdateBoardDto updateBoard,CancellationToken cancellationToken = default(CancellationToken));
    Task DeleteBoardAsync(string id, CancellationToken cancellationToken = default(CancellationToken));
    Task<IEnumerable<GetBoardDto>> GetBoardsAsync(CancellationToken cancellationToken = default(CancellationToken));
    Task<GetBoardDto> GetBoardByIdAsync(string id, CancellationToken cancellationToken = default(CancellationToken));

}
