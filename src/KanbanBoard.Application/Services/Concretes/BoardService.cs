using KanbanBoard.Application.Dtos.BoardDtos;
using KanbanBoard.Application.Services.Interface;
using KanbanBoard.Infrastructure.Repositories.Manager;

namespace KanbanBoard.Application.Services.Concretes;

public class BoardService : IBoardService
{
    private readonly IRepositoryManager _repository;
    public BoardService(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<GetBoardDto> GetBoardByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var board = await _repository.Board.FindByIdAsync(id, cancellationToken);
        return new GetBoardDto
        {
            Id = board.Id,
            Title = board.Title,
            Description = board.Description
        };
    }

    public async Task<IEnumerable<GetBoardDto>> GetBoardsAsync(CancellationToken cancellationToken = default)
    {
        var boards = await _repository.Board.FindAllAsync(cancellationToken,includes:x=>x.Statuses);
        return boards.Select(x => new GetBoardDto
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
        }).ToList();
    }
}
