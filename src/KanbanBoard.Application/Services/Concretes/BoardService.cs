using KanbanBoard.Application.Dtos.BoardDtos;
using KanbanBoard.Application.Services.Interface;
using KanbanBoard.Core.Models;
using KanbanBoard.Infrastructure.Repositories.Manager;

namespace KanbanBoard.Application.Services.Concretes;

public class BoardService : IBoardService
{
    private readonly IRepositoryManager _repository;
    public BoardService(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task CreateBoardAsync(CreateBoardDto createBoard, CancellationToken cancellationToken = default)
    {
        var board = new Board
        {
            Title = createBoard.Title,
            Description = createBoard.Description
        };

        _repository.Board.Insers(board);
        await _repository.SaveAsync(cancellationToken);
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
        var boards = await _repository.Board.FindAllAsync(cancellationToken, includes: x => x.Statuses);
        return boards.OrderByDescending(x=>x.CreatedDate).Select(x => new GetBoardDto
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
        }).ToList();
    }
}
