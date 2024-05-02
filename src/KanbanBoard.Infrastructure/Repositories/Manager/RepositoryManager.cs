using KanbanBoard.Infrastructure.EFContext;
using KanbanBoard.Infrastructure.Repositories.Interfaces;

namespace KanbanBoard.Infrastructure.Repositories.Manager;

public class RepositoryManager : IRepositoryManager
{
    private readonly EfDbContext _dbContext;
    private readonly IIssueRepository _issue;
    private readonly IBoardRepository _board;
    private readonly IStatusRepository _status;
    private readonly ICommentRepository _comment;

    public RepositoryManager(IIssueRepository issue, EfDbContext dbContext, IBoardRepository board, IStatusRepository status, ICommentRepository comment)
    {
        _dbContext = dbContext;
        _issue = issue;
        _board = board;
        _status = status;
        _comment = comment;
    }

    public IIssueRepository Issue => _issue;
    public IBoardRepository Board => _board;
    public IStatusRepository Status => _status;
    public ICommentRepository Comment => _comment;

    public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();
    public async Task<int> SaveAsync(CancellationToken cancellationToken = default) => await _dbContext.SaveChangesAsync(cancellationToken);
 
}
