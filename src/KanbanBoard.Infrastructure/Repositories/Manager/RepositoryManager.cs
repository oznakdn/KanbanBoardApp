using KanbanBoard.Infrastructure.EFContext;
using KanbanBoard.Infrastructure.Repositories.Interfaces;

namespace KanbanBoard.Infrastructure.Repositories.Manager;

public class RepositoryManager : IRepositoryManager
{
    private readonly IIssueRepository _issue;
    private readonly EfDbContext _dbContext;

    public RepositoryManager(IIssueRepository issue, EfDbContext dbContext)
    {
        _dbContext = dbContext;
        _issue = issue;
    }

    public IIssueRepository Issue => _issue;

    public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();
    public async Task<int> SaveAsync(CancellationToken cancellationToken = default) => await _dbContext.SaveChangesAsync(cancellationToken);
 
}
