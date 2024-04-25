using KanbanBoard.Core.Models;

namespace KanbanBoard.Infrastructure.Repositories.Interfaces;

public interface IIssueRepository : IRepository<Issue>
{
    Task<int> GetNextOrderAsync(string statusId);
}
