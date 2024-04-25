using KanbanBoard.Application.Dtos.BoardDtos;
using KanbanBoard.Application.Dtos.IssueDtos;
using KanbanBoard.Application.Dtos.StatusDtos;
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

    public async Task<IEnumerable<GetBoardDto>> GetBoardsAsync(CancellationToken cancellationToken = default)
    {
        var boards = await _repository.Board.FindAllAsync(cancellationToken,includes:x=>x.Statuses);
        return boards.Select(x => new GetBoardDto
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            //Status = x.Statuses.Select(y => new GetStatusDto
            //{
            //    Id = y.Id,
            //    Name = y.Name,
            //    BoardId = y.BoardId,
            //    Issues = y.Issues.Select(z => new GetIssueDto
            //    {
            //        Id = z.Id,
            //        StatusId = z.StatusId,
            //        Summary = z.Summary,
            //        Description = z.Description,
            //        IssueType = z.IssueType,
            //        Order = z.Order,
            //        Priority = z.Priority
            //    }).ToList()
            //}).ToList()
        }).ToList();
    }
}
