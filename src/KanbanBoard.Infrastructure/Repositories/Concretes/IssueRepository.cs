using KanbanBoard.Core.Models;
using KanbanBoard.Infrastructure.EFContext;
using KanbanBoard.Infrastructure.Repositories.Abstract;
using KanbanBoard.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KanbanBoard.Infrastructure.Repositories.Concretes;

public class IssueRepository : AbstractRepository<Issue>, IIssueRepository
{
    public IssueRepository(EfDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<int> GetNextOrderAsync(string statusId)
    {
        var lastOrder = await _dbSet.Where(x => x.StatusId == statusId).MaxAsync(x => x.Order);
        return lastOrder + 1;
    }
}
